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

        public Student CheckingStudent(LoginModel loginModel)
        {
            try
            {
                Student student = StudentHelper.ConvertLoginModelToStudent(loginModel);
                Student isStudent = _context.Student.FirstOrDefault(m => m.Username == student.Username && m.Password == student.Password);
                
                if(isStudent != null)
                {
                    return isStudent;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckingStudentExist(LoginModel _loginUserModel)
        {
            try
            {
                Student student = StudentHelper.ConvertLoginModelToStudent(_loginUserModel);

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

        public Teacher CheckingTeacher(LoginModel loginModel)
        {
            try
            {
                Teacher teacher = TeacherHelper.ConvertLoginModelToTeacher(loginModel);
                Teacher isTeacher = _context.Teacher.FirstOrDefault(m => m.Username == teacher.Username && m.Password == teacher.Password);
                
                if(isTeacher != null)
                {
                    return isTeacher;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckingTeacherExist(LoginModel _loginUserModel)
        {
            try
            {
                Teacher teacher = TeacherHelper.ConvertLoginModelToTeacher(_loginUserModel);
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
