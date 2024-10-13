using Microsoft.AspNetCore.Mvc;

namespace Safad.Controllers
{
    public class CoachController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
