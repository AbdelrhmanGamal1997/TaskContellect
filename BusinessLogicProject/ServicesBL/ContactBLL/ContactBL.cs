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

        public async Task<List<ContactViewModel>> GetContacts(int pageNumber=1, int pageSize=5, string? searchWord=null)
        {
            IEnumerable<Contact> contacts;
            if (searchWord != null)
                contacts = await _unitOfWork.Contacts.GetAllByExpressionAsync(x => x.Name.Contains(searchWord) || x.Address.Contains(searchWord) || x.Phone.Contains(searchWord),pageNumber,pageSize);
            else
                contacts = await _unitOfWork.Contacts.GetAllByExpressionAsync(null,pageNumber,pageSize);

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

        public async Task<ContactUpdateDto> getContactById(int id)
        { 
            Contact con=await _unitOfWork.Contacts.GetByIdAsync(id);
            if (con == null)
                return null;
            ContactUpdateDto contact = new ContactUpdateDto() { 
                Address = con.Address,
                Id = con.Id,
                Name=con.Name,
                Phone=con.Phone,
                Notes=con.Notes
            };
            return contact;
        }

        public async Task<bool> EditContact(ContactUpdateDto contactUpdate)
        {
            Contact con = await _unitOfWork.Contacts.GetByIdAsync(contactUpdate.Id);
            if (con == null)
                return false;

            con.Name = contactUpdate.Name;
            con.Address = contactUpdate.Address;
            con.Phone = contactUpdate.Phone;
            con.Notes = contactUpdate.Notes;
            int res = await _unitOfWork.CompleteAsync();
            return res >= 0 ? true : false;
        }

        public async Task<bool> DeleteContact(int Id)
        {
            Contact con = await _unitOfWork.Contacts.GetByIdAsync(Id);
            if (con == null)
                return false;

            _unitOfWork.Contacts.Delete(con);
            int res = await _unitOfWork.CompleteAsync();
            return res >= 0 ? true : false;
        }
    }
}
