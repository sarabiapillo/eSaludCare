using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApp.Controllers
{
    /// <summary>
    /// Controlador para la página principal de citas médicas.
    /// </summary>
    public class CitasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
