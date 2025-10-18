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

        [HttpGet]
        public IActionResult Index()
        {
            var departamentos = _context.Departamentos.OrderByDescending(d => d.FechaCreacion).ToList();
            ViewBag.Departamentos = departamentos;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear()
        {
            var nombre = Request.Form["nombre"];
            var siglas = Request.Form["siglas"];
            var descripcion = Request.Form["descripcion"];

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(siglas))
            {
                TempData["Error"] = "Nombre y siglas son obligatorios.";
                return RedirectToAction("Index");
            }

            if (_context.Departamentos.Any(d => d.Nombre == nombre || d.Siglas == siglas))
            {
                TempData["Error"] = "Ya existe un departamento con ese nombre o siglas.";
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
       