using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;

namespace Taskmanagement_Repository.Interface
{
    public interface ILoginRepository
    {
        bool CheckingStudentExist(LoginModel _loginUserModel);

        bool CheckingTeacherExist(LoginModel _loginUserModel);

        Student CheckingStudent(LoginModel loginModel);

        Teacher CheckingTeacher(LoginModel loginModel);

    }
}
