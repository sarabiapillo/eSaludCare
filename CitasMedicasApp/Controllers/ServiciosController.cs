using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApp.Controllers
{
    public class ServiciosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
