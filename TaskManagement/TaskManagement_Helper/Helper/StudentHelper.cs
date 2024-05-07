using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;

namespace TaskManagement_Helper.Helper
{
    public class StudentHelper
    {
        public static Student ConvertRegisterModelToStudent(RegisterModel registerModel)
        {
            try
            {
                Student student = new Student();
                student.Username = registerModel.Username;
                student.Email = registerModel.Email;
                student.Password = registerModel.Password;
                student.Address = registerModel.Address;
                student.ContactNumber = registerModel.ContactNumber;
                student.CityID = StringToIntConversion(registerModel.CityId);
                student.StateID = StringToIntConversion(registerModel.StateId);

                return student;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int StringToIntConversion(string _CityId)
        {
            int _City_Id = Convert.ToInt32(_CityId);
            return _City_Id;
        }
    }
}
