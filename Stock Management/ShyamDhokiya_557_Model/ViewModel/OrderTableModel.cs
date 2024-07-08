using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Model.ViewModel
{
    public class OrderTableModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Email { get; set; } 
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
    }
}
