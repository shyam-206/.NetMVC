using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Model.ViewModel
{
    public class UserModel
    {
        public int user_id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6,ErrorMessage = "Please provide a password min length 6")]
        public string password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [MinLength(6, ErrorMessage = "Please provide a confirmPassword min length 6")]
        public string confirmPassword { get; set; }

    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Please provide a password min length 6")]
        public string password { get; set; }

    }
}
