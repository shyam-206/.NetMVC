using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Model.ViewModel
{
    public class BikeModel
    {
        public int BikeId { get; set; }
        public int SellerId { get; set; }
        [Required(ErrorMessage = "Please Enter Brnad Name")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Please Enter Model Name")]
        public string Models { get; set; }
        [Required(ErrorMessage = "Please Enter Years")]
        public int Years { get; set; }
        [Required(ErrorMessage = "Please Enter KM")]
        public int kilometresDriven { get; set; }
        [Required(ErrorMessage = "Please Enter Price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter Image")]
        public string Image { get; set; }
        
        [Required(ErrorMessage = "Please Enter Seller First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Seller Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [MaxLength(10,ErrorMessage = "Enter valid Number")]
        public string PhoneNumber { get; set; }
        public string[] BikeImages { get; set; }

        public bool IsFavourite { get; set; } 
    }
}
