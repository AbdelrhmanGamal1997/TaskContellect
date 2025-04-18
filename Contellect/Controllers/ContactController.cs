using Microsoft.AspNetCore.Mvc;

namespace Contellect.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult ContactGetAll()
        {
            return View();
        }
    }
}
