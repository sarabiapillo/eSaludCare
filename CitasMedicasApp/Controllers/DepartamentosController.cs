using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApp.Controllers
{
    public class DepartamentosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
