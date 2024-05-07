using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;

namespace TaskManagement_Helper.Helper
{
    public class TeacherHelper
    {

        /*CONVERT THE REGISTER MODEL TO TEACHER*/
        public static Teacher ConvertRegisterModelToTeacher(RegisterModel registerModel)
        {
            try
            {
                Teacher teacher = new Teacher();
                teacher.Username = registerModel.Username;
                teacher.Email = registerModel.Email;
                teacher.Password = registerModel.Password;
                teacher.Address = registerModel.Address;
                teacher.ContactNumber = registerModel.ContactNumber;
                teacher.CityID = StringToIntConversion(registerModel.CityId);
                teacher.StateID = StringToIntConversion(registerModel.StateId);

                return teacher;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /*FOR THE CONVERT THE STRING TO INT OF THE REGISTER MODEL CITY AND STATE ID*/
        public static int StringToIntConversion(string _CityId)
        {
            int _City_Id = Convert.ToInt32(_CityId);
            return _City_Id;
        }
    }
}
