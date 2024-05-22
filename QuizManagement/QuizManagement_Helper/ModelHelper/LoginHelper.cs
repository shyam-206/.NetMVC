using QuizManagement_Model.DbContext;
using QuizManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
