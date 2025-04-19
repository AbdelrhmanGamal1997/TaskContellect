using BusinessLogicProject.ServicesBL.ContactBLL;
using BusinessLogicProject.ViewModels.ContactDtos;
using BusinessLogicProject.ViewModels.UserDtos;
using Contellect.SignlR;
using CoreEntities.Enities;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Contellect.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactBL _contactBL;
        private readonly IHubContext<ContactHub> _hubContext;
        public ContactController(IContactBL contactBL, IHubContext<ContactHub> contactHub)
        {
            _contactBL = contactBL;
            _hubContext = contactHub;
        }
        public async Task<IActionResult> ContactGetAll()
        {
            var data=await _contactBL.GetContacts();
            return View(data);
        }
        [HttpGet("Contact/ContactGetAllDataBySearch")]
        public async Task<IActionResult> ContactGetAllDataBySearch(int pageNumber, int pageSize, string? searchWord = null)
        {
            var data = await _contactBL.GetContacts(pageNumber, pageSize, searchWord);
            return Json(data);
        }

        public IActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContact(ContactCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var res= await _contactBL.AddContact(dto);
                if (res == true)
                {
                    TempData["Success"] = "Contact added successfully!";
                    return RedirectToAction("ContactGetAll");
                }
                else
                {
                    TempData["Error"] = "Server Error";
                    return RedirectToAction("ContactGetAll");
                }
            }
            TempData["Error"] = "Failed to add contact. Please check the form.";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> EditContact(int id)
        {
            var con=await _contactBL.getContactById(id);
            return View(con);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact(ContactUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                bool res = await _contactBL.EditContact(dto);
                if (res == true)
                {
                    TempData["Success"] = "Contact Update successfully!";
                    // Notify all clients viewing this contact
                    await _hubContext.Clients.Group(dto.Id.ToString())
                        .SendAsync("ReceiveContactUpdate", new
                        {
                            id = dto.Id,
                            name = dto.Name,
                            address = dto.Address,
                            phone = dto.Phone,
                            notes = dto.Notes
                        });
                    return RedirectToAction("ContactGetAll");
                }
                else
                {
                    TempData["Error"] = "Server Error";
                    return RedirectToAction("ContactGetAll");
                }
              
            }
            TempData["Error"] = "Failed to update contact. Please check the form.";
            return View(dto);
        }

        //[HttpDelete]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var res = await _contactBL.DeleteContact(id);
            if (res == true)
            {
                TempData["Success"] = "Contact delete successfully!";
                return RedirectToAction("ContactGetAll");
            }

            TempData["Error"] = "Server Error";
            return RedirectToAction("ContactGetAll");

        }

    }
}
