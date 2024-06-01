using CourseManagement_Model.DBContext;
using CourseManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement_Helper.Helper
{
    public class LoginHelper
    {
        public static Users ConvertUserModelToUser(UserModel userModel)
        {
            try
            {
                Users user = new Users
                {
                    Username = userModel.Username,
                    Email = userModel.Email,
                    Password = userModel.Password,
                    Role = userModel.Role,
                    Created_at = DateTime.Now
                };
                return user != null ? user : null;
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
                UserModel userModel = new UserModel
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role
                };

                return userModel != null ? userModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
