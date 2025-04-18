using Microsoft.AspNetCore.Mvc;

namespace Contellect.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
