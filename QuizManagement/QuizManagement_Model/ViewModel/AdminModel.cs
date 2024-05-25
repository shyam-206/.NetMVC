using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Model.ViewModel
{
    public class AdminModel
    {
        public int admin_id { get; set; }
        [Required(ErrorMessage = "Please Enter a Username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please Enter a email")]
        [EmailAddress(ErrorMessage = "Please Enter a Valid email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Please provide a password min length 6")]
        public string password { get; set; }
    }
}
