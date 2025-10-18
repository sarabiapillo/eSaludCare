using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Agregar este using para Include
using CitasMedicasApp.Data;
using CitasMedicasApp.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CitasMedicasApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        
        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        private string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string correo, string contrasena)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                ViewBag.Mensaje = "Correo y contraseña son requeridos";
                return View();
            }

            var hashed = HashPassword(contrasena);
            var usuario = _context.Usuarios
                .Include(u => u.Rol)
                // Si tienes migraciones pendientes, asegúrate de haber ejecutado:
                // dotnet ef migrations add AddFotoPerfilToUsuario
                // dotnet ef database update
                // para que la columna FotoPerfil exista en la base de datos.
                .FirstOrDefault(u => u.Correo == correo && u.Contrasena == hashed);

            if (usuario != null)
            {
                if (usuario.Rol != null)
                {
                    // Redirecciones corregidas
                    if (usuario.Rol.Nombre == "Cliente")
                        return RedirectToAction("Index", "Cliente", new { area = "Cliente" });
                    if (usuario.Rol.Nombre == "Administrador")
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    if (usuario.Rol.Nombre == "Doctor")
                        return RedirectToAction("Index", "Medicos", new { area = "Admin" });
                }
            }
            
            ViewBag.Mensaje = "Credenciales incorrectas";
            return View();
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registro(string nombre, string apellido, string correo, string contrasena)
        {
            // Validaciones básicas
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || 
                string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                ViewBag.Mensaje = "Todos los campos son requeridos";
                return View();
            }

            // Verificar si el correo ya existe
            if (_context.Usuarios.Any(u => u.Correo == correo))
            {
                ViewBag.Mensaje = "El correo ya está registrado";
                return View();
            }

            var clienteRol = _context.Roles.FirstOrDefault(r => r.Nombre == "Cliente");
            if (clienteRol == null)
            {
                ViewBag.Mensaje = "No existe el rol Cliente";
                return View();
            }

            var usuario = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                Contrasena = HashPassword(contrasena),
                RolId = clienteRol.RolId
            };

            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                ViewBag.Mensaje = "Registro exitoso";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error en el registro: " + ex.Message;
                return View();
            }
        }
    }
}