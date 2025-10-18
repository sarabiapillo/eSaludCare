using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApp.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class CitasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
