using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Model.ViewModel
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int SuppierId { get; set; }
        public string SuppierName { get; set; }
        public string SuppierEmail { get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetailModel> OrderDetailList { get; set; }
    }
}
