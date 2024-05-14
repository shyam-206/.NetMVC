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
        /*CONVERT THE REGISTER MODEL TO STUDENT*/
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

        /*FOR THE CONVERT THE STRING TO INT OF THE REGISTER MODEL CITY AND STATE ID*/
        public static int StringToIntConversion(string _CityId)
        {
            int _City_Id = Convert.ToInt32(_CityId);
            return _City_Id;
        }

        public static Student ConvertLoginModelToStudent(LoginModel loginModel)
        {
            try
            {
                Student student = new Student();
                student.Username = loginModel.Username;
                student.Password = loginModel.Password;
                return student;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<StudentModel> ConvertStudentToStudentModel(List<Student> studentList)
        {
            try
            {
                List<StudentModel> studentModelList = new List<StudentModel>();
                if(studentList != null)
                {
                    foreach(var item in studentList)
                    {
                        StudentModel studentModel = new StudentModel();
                        studentModel.StudentID = item.StudentID;
                        studentModel.Username = item.Username;
                        studentModel.Password = item.Password;
                        studentModel.Email = item.Email;
                        studentModel.Address = item.Address;
                        studentModel.CityID = item.CityID;
                        studentModel.StateID = item.StateID;
                        studentModelList.Add(studentModel);
                    }
                }
                return studentModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Student> ConvertAssignmentListTostudentList(List<Assignment> assignments)
        {
            try
            {
                List<Student> students = new List<Student>();
                if(assignments != null)
                {
                    foreach(var assignment in assignments)
                    {
                        Student student = new Student();
                        student.StudentID = assignment.Student.StudentID;
                        student.Username = assignment.Student.Username;
                    }
                    
                }
                return students;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
