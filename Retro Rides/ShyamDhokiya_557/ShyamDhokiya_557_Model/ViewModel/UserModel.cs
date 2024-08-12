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
        [Required(ErrorMessage = "Please FirstName is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Last Name is Required")]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please email is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Mobile Number is Required")]
        [MaxLength(10,ErrorMessage = "Please Enter Valid Number")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Please Date Of Birth is Required")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Please Address is Required")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required(ErrorMessage = "Please Country is Required")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Please State is Required")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Please City is Required")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "Please Zipcode is Required")]
        [MaxLength(6, ErrorMessage = "Please Enter Valid zipcode")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Please Profile Photo is Required")]
        public string Image { get; set; }
        public string Password { get; set; }

        public string FormateDate => DOB.ToString("dd-MM-yyyy");

        public string Token { get; set; }
    }
}
