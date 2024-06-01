using CourseManagement_Helper.Helper;
using CourseManagement_Model.DBContext;
using CourseManagement_Model.ViewModel;
using CourseManagement_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Repository.Service
{
    public class LoginService : ILoginRepository
    {
        private readonly CourseManagement557Entities _context = new CourseManagement557Entities();
        public bool AddUser(UserModel userModel)
        {
            Users user = new Users();
            user = LoginHelper.ConvertUserModelToUser(userModel);
            int CheckUserSave = 0;

            _context.Users.Add(user);
            CheckUserSave = _context.SaveChanges();

           
            return CheckUserSave > 0 ? true : false;
        }

        public UserModel CheckUserExist(LoginModel loginModel)
        {
            try
            {
                Users user = new Users();
                user = _context.Users.Where(m => m.Email == loginModel.Email && m.Role == loginModel.Role && m.Password == loginModel.Password).FirstOrDefault();
                UserModel userModel = LoginHelper.ConvertUserToUserModel(user);

                return userModel != null ? userModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
