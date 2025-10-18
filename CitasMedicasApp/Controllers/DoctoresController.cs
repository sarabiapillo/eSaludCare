using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApp.Controllers
{
    public class DoctoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
