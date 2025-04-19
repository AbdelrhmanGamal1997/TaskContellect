using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicProject.ViewModels.ContactDtos
{
    public class ContactBaseDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "phone is required.")]
        [RegularExpression(@"^01\d{9}$", ErrorMessage = "Phone number must start with '01' and be exactly 11 digits.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }
       
    }
}
