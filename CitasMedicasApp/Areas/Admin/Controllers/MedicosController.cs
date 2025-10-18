using Microsoft.AspNetCore.Mvc;
using CitasMedicasApp.Data;
using CitasMedicasApp.Models;
using System.Linq;

namespace CitasMedicasApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MedicosController : Controller
    {
        private readonly AppDbContext _context;
        public MedicosController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var medicos = _context.Medicos
                .Select(m => new
                {
                    m.IdMedico,
                    m.Usuario.Nombre,
                    m.Usuario.Apellido,
                    m.Usuario.Correo,
                    m.Especialidad,
                    m.NumeroCedula,
                    m.ExperienciaAnios,
                    Departamento = m.Departamento != null ? m.Departamento.Nombre : "",
                    m.FechaContratacion
                })
                .ToList();
            ViewBag.Medicos = medicos;
            ViewBag.Departamentos = _context.Departamentos.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CrearMedico(string nombre, string apellido, string correo, string contrasena, string especialidad, string numeroCedula, int experienciaAnios, int? idDepartamento, string fechaContratacion)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena) || string.IsNullOrWhiteSpace(especialidad) || string.IsNullOrWhiteSpace(numeroCedula))
            {
                TempData["Error"] = "Todos los campos obligatorios deben estar completos.";
                return RedirectToAction("Index");
            }

            var rolMedico = _context.Roles.FirstOrDefault(r => r.Nombre == "Doctor");
            if (rolMedico == null)
            {
                TempData["Error"] = "No existe el rol Doctor";
                return RedirectToAction("Index");
            }

            var usuario = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                Contrasena = HashPassword(contrasena),
                RolId = rolMedico.RolId
            };
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            var medico = new Medico
            {
                IdUsuario = usuario.UsuarioId,
                Especialidad = especialidad,
                NumeroCedula = numeroCedula,
                ExperienciaAnios = experienciaAnios,
                IdDepartamento = idDepartamento,
                FechaContratacion = string.IsNullOrEmpty(fechaContratacion) ? null : DateTime.Parse(fechaContratacion)
            };
            _context.Medicos.Add(medico);
            _context.SaveChanges();

            TempData["Mensaje"] = "Médico creado correctamente";
            return RedirectToAction("Index");
        }

        private string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
