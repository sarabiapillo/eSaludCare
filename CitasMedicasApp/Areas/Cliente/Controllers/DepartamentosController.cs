using Microsoft.AspNetCore.Mvc;
using CitasMedicasApp.Data;
using System.Linq;

namespace CitasMedicasApp.Areas.Cliente.Controllers
{
    [Area("Cliente")]
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
        public IActionResult Crear(string nombre, string siglas, string descripcion)
        {
            var departamento = new Models.Departamento
            {
                Nombre = nombre,
                Siglas = siglas,
                Descripcion = descripcion
            };
            _context.Departamentos.Add(departamento);
            _context.SaveChanges();
            TempData["Mensaje"] = "Departamento creado correctamente";
            return RedirectToAction("Index");
        }
    }
}
