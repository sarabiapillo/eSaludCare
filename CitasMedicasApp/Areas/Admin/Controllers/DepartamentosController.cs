using Microsoft.AspNetCore.Mvc;
using CitasMedicasApp.Data;
using CitasMedicasApp.Models;
using System.Linq;

namespace CitasMedicasApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartamentosController : Controller
    {
        private readonly AppDbContext _context;
        public DepartamentosController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var departamentos = _context.Departamentos.ToList();
            ViewBag.Departamentos = departamentos;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(string nombre, string siglas, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(siglas))
            {
                TempData["Error"] = "Nombre y siglas son obligatorios.";
                return RedirectToAction("Index");
            }
            var departamento = new Departamento
            {
                Nombre = nombre,
                Siglas = siglas,
                Descripcion = descripcion,
                FechaCreacion = DateTime.Now
            };
            _context.Departamentos.Add(departamento);
            _context.SaveChanges();
            TempData["Mensaje"] = "Departamento creado correctamente";
            return RedirectToAction("Index");
        }
    }
}
