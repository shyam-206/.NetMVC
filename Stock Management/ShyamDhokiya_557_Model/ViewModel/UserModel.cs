using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Model.ViewModel
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter name")]
        
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter password")]
        [MinLength(6, ErrorMessage = "Please enter password 6 length")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Confirm password")]
        [MinLength(6, ErrorMessage = "Please enter password 6 length")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
