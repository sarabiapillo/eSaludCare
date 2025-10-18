using Microsoft.AspNetCore.Mvc;
using CitasMedicasApp.Data;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace CitasMedicasApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PerfilController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PerfilController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Suponiendo que el admin está autenticado y su id es 1 (ajusta según tu lógica de autenticación)
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Rol.Nombre == "Administrador");
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(string nombre, IFormFile fotoPerfil)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Rol.Nombre == "Administrador");
            if (usuario == null)
            {
                TempData["Error"] = "Usuario no encontrado.";
                return RedirectToAction("Index");
            }

            if (!string.IsNullOrWhiteSpace(nombre))
                usuario.Nombre = nombre;

            if (fotoPerfil != null && fotoPerfil.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = $"admin_{usuario.UsuarioId}_{Path.GetFileName(fotoPerfil.FileName)}";
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    fotoPerfil.CopyTo(stream);
                }
                usuario.FotoPerfil = "/uploads/" + fileName;
            }

            _context.SaveChanges();
            TempData["Mensaje"] = "Perfil actualizado correctamente";
            return RedirectToAction("Index");
        }
    }
}
