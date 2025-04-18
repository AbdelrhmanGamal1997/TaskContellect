using BusinessLogicProject.ServicesBL.ContactBLL;
using BusinessLogicProject.ViewModels.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contellect.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactBL _contactBL;
        public ContactController(IContactBL contactBL)
        {
            _contactBL = contactBL;
        }
        public IActionResult ContactGetAll()
        {
            return View();
        }
        public IActionResult AddContact()
        {
            return View();
        }
        // POST: /Employee/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContact(ContactCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _contactBL.AddContact(dto);
                TempData["Success"] = "Contact added successfully!";
                return RedirectToAction("ContactGetAll");
            }
            TempData["Error"] = "Failed to add contact. Please check the form.";
            return View(dto);
        }
    }
}
