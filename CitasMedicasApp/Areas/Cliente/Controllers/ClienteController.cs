using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApp.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
