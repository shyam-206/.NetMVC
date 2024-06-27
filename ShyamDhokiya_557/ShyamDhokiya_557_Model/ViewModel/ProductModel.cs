using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Model.ViewModel
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Enter Product Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Product Desription")]
        public string Descrption { get; set; }
        [Required(ErrorMessage = "Enter Product Type")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Enter Product price")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Enter Product Quanity")]
        public int Quantity { get; set; }
        public bool IsAddCart { get; set; }
    }
}
