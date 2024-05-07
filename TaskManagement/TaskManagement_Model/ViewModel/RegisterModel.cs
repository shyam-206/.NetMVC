using System;
using System.Collections.Generic;
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
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public string ContactNumber { get; set; }

        public string StateId { get; set; }

        public string CityId { get; set; }

        public UserRole Role { get; set; }
    }
}
