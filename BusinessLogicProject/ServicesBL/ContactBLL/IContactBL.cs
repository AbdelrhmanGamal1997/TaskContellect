using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicProject.ViewModels.ContactDtos;
using BusinessLogicProject.ViewModels.UserDtos;

namespace BusinessLogicProject.ServicesBL.ContactBLL
{
    public interface IContactBL
    {
        Task<List<ContactViewModel>> GetContacts(string? searchWord=null);
        Task<bool> AddContact(ContactCreateDto dto);
    }
}
