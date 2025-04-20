using BusinessLogicProject.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Contellect.Controllers
{
    public class AccountController : Controller
    {

        List<LoginDto> loginDtos = new List<LoginDto>{
            new LoginDto { UserName="user1",Password="user1" },
            new LoginDto { UserName="user2",Password="user2" }
            };
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDto dto)
        {
            // Hardcoded credentials
            if (loginDtos.Any(x=>x.UserName==dto.UserName && x.Password==dto.Password))
                return RedirectToAction("ContactGetAll", "Contact");
            else
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }
        }

    }
}
