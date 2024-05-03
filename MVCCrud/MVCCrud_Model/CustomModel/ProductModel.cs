using System;
using System.ComponentModel.DataAnnotations;

namespace MVCCrud_Model.CustomModel
{
    public class ProductModel
    {
        public int Product_Id { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Please provide a product Name")]
        public string PName { get; set; }

        [Required(ErrorMessage = "Please provide a price")]
        public Nullable<int> Price { get; set; }

        [Required(ErrorMessage = "Please provide a Quantity")]
        public Nullable<int> Quantity { get; set; }
    }
}
