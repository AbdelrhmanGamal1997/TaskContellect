using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicProject.ViewModels.ContactDtos
{
    public class CantactBaseDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
    }
}
