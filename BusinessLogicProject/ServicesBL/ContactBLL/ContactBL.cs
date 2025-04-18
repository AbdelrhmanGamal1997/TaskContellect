using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicProject.ServicesBL.ContactBLL;
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
