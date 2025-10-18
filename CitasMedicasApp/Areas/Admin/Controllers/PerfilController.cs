using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PerfilController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
