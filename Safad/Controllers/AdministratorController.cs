using Microsoft.AspNetCore.Mvc;

namespace Safad.Controllers
{
    public class AdministratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
