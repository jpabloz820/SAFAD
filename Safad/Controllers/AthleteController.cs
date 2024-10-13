using Microsoft.AspNetCore.Mvc;

namespace Safad.Controllers
{
    public class AthleteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
