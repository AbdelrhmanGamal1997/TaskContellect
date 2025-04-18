using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicProject.ServicesBL.ContactBLL;
using BusinessLogicProject.ViewModels.ContactDtos;
using BusinessLogicProject.ViewModels.UserDtos;
using CoreEntities.Enities;
using Repository.UnitOfWork;

namespace BusinessLogicProject.ServicesBL.UserBLL
{
    public class ContactBL : IContactBL
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ContactViewModel>> GetContacts(string? searchWord=null)
        {
            IEnumerable<Contact> contacts;
            if (searchWord != null)
                contacts = await _unitOfWork.Contacts.GetAllByExpressionAsync(x => x.Name.Contains(searchWord) || x.Address.Contains(searchWord) || x.Phone.Contains(searchWord));
            else
                contacts = await _unitOfWork.Contacts.GetAllByExpressionAsync();

            return contacts.Select(x => new ContactViewModel
            {
                Address = x.Address,
                Id = x.Id,
                Name=x.Name,
                Phone=x.Phone
            }).ToList();
        }
        public async Task<bool> AddContact(ContactCreateDto dto)
        {
            await _unitOfWork.Contacts.AddAsync(new Contact
            {
                Name = dto.Name,
                Address = dto.Address,
                Notes = dto.Notes,
                Phone = dto.Phone,
            });
            int res = await _unitOfWork.CompleteAsync();
            return res >= 0 ? true : false;
        }
    }
}
