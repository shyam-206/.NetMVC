using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Model.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please Enter a email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter a password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select a role ?")]
        public string Role { get; set; }
    }
}
