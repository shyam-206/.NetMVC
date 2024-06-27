using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement_Model.ViewModel
{
    public enum UserRole
    {
        Student,
        Teacher
    }
    public class RegisterModel
    {

        [Required(ErrorMessage = "Username is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Please Provide more than 8 length")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please provide a contact number")]
        [MaxLength(10, ErrorMessage = "Contact number should be not be more than 10 digit")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Please Select State")]
        public string StateId { get; set; }
        [Required(ErrorMessage = "Please select City")]
        public string CityId { get; set; }

        [Required(ErrorMessage = "Please select your role")]
        public UserRole Role { get; set; }

        
    }
}
