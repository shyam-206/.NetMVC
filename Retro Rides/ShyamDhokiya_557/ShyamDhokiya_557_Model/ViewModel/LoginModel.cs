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
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please email is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please select Role")]
        public string Role { get; set; }
    }
}
