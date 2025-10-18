using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApp.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class PerfilController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
