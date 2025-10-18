using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(string correo, string contrasena)
        {
            var hashed = HashPassword(contrasena);
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == correo && u.Contrasena == hashed);
            if (usuario != null)
            {
                if (usuario.RolId == 1) // Cliente
                    return RedirectToAction("Index", "Cliente", new { area = "Cliente" });
                if (usuario.RolId == 2) // Administrador
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                // Puedes agregar lógica para doctor si lo necesitas
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
        public IActionResult Registro(string nombre, string apellido, string correo, string contrasena)
        {
            // Solo rol cliente
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
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            ViewBag.Mensaje = "Registro exitoso";
            return View();
        }
    }
}
