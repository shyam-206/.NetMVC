using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Model.ViewModel
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter a username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Enter a Email")]
        [EmailAddress(ErrorMessage = "Please Enter a valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter a Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Confim Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please select role ?")]
        public string Role { get; set; }
    }
}
