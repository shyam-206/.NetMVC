using System.ComponentModel.DataAnnotations;

namespace MVCCrud_Model.CustomModel
{
    public class RegisterModel
    {
        public int UserID { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please Provide a Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please Provide a valid Email Address")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please provide a username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please provide a password")]
        public string Password { get; set; }
    }
}
