using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement_Model.ViewModel
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Username is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Please Provide more than 8 length")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select your role")]
        public UserRole Role { get; set; }
    }
}
