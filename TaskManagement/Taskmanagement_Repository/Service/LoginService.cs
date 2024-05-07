using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Helper.Helper;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;
using Taskmanagement_Repository.Interface;

namespace Taskmanagement_Repository.Service
{
    public class LoginService : ILoginRepository
    {
        private readonly TaskManagement_557Entities _context;

        public LoginService()
        {
            _context = new TaskManagement_557Entities();
        }
        public bool CheckingStudentExist(RegisterModel _loginUserModel)
        {
            try
            {
                Student student = StudentHelper.ConvertRegisterModelToStudent(_loginUserModel);

                Student isUserExist = _context.Student.FirstOrDefault(m => m.Username == student.Username && m.Password == student.Password);

                if (isUserExist != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckingTeacherExist(RegisterModel _loginUserModel)
        {
            try
            {
                Teacher teacher = TeacherHelper.ConvertRegisterModelToTeacher(_loginUserModel);
                Teacher isUserExist = _context.Teacher.FirstOrDefault(m => m.Username == teacher.Username && m.Password == teacher.Password);

                if (isUserExist != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
