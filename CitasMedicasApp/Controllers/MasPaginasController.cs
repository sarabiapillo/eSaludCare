using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApp.Controllers
{
    public class MasPaginasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
