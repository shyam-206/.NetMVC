using QuizManagement_Model.DbContext;
using QuizManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Helper.ModelHelper
{
    public class LoginHelper
    {
        public static Users ConvertUserModelToUser(UserModel userModel)
        {
            try
            {
                Users user = new Users();
                if(userModel != null)
                {
                    user.user_id = userModel.user_id;
                    user.username = userModel.username;
                    user.email = userModel.email;
                    user.password = userModel.password;
                    user.created_at = DateTime.Now;
                }

                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Users ConvertLoginModelToLogin(LoginModel loginModel)
        {
            try
            {
                Users user = new Users();
                if (loginModel != null)
                {
                    user.email = loginModel.email;
                    user.password = EncryptPassword(loginModel.password);
                }
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public static Admin ConvertLoginModelToAdmin(LoginModel loginModel)
        {
            try
            {
                Admin admin = new Admin();
                if (loginModel != null)
                {
                    admin.email = loginModel.email;
                    admin.password = loginModel.password;
                }
                return admin;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static AdminModel ConvertAdminToAdminModel(Admin admin)
        {
            try
            {
                AdminModel adminModel = new AdminModel();
                adminModel.admin_id = admin.admin_id;
                adminModel.username = admin.username;
                adminModel.email = admin.email;
                adminModel.password = admin.password;
                return adminModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static UserModel ConvertUserToUserModel(Users user)
        {
            try
            {
                UserModel userModel = new UserModel();
                userModel.user_id = user.user_id;
                userModel.username = user.username;
                userModel.email = user.email;
                userModel.password = user.password;
                return userModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string EncryptPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                if (password != null)
                {
                    var hashPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(hashPassword);
                }

                return null;
            }

        }
        
    }
}
