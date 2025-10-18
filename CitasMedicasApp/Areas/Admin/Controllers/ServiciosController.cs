using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiciosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
