using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement_Model.ViewModel
{
    public class CityModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public Nullable<int> StateID { get; set; }
    }
}
