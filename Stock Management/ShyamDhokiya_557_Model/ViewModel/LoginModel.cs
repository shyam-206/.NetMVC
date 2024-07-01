using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Model.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please Enter email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter password")]
        [MinLength(6,ErrorMessage = "Please enter password 6 length")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Role")]
        public string Role { get; set; }
    }
}
