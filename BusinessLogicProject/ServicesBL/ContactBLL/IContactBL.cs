using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicProject.ViewModels.UserDtos;

namespace BusinessLogicProject.ServicesBL.ContactBLL
{
    public interface IContactBL
    {
        Task<bool> AddContact(ContactCreateDto dto);
    }
}
