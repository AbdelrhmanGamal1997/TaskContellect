using System.Security.Claims;
using BusinessLogicProject.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
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
        public async Task<ActionResult> LoginAsync(LoginDto dto)
        {
            // Hardcoded credentials
            if (loginDtos.Any(x => x.UserName == dto.UserName && x.Password == dto.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, dto.UserName),
                    new Claim("UserRole", "Admin")
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", principal);

                return RedirectToAction("ContactGetAll", "Contact");
            }
            else
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }
        }

    }
}
