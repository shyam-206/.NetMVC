using ShyamDhokiya_557_Model.DbContext;
using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Helper.Helper
{
    public class UserHelper
    {
        public static UserModel ConvertUserToUserModel(Suppiers suppier)
        {
            try
            {
                UserModel userModel = new UserModel
                {
                    UserId = suppier.SuppierId,
                    Name = suppier.Name,
                    Email = suppier.Email,
                    Password = suppier.Password
                };

                return userModel != null ? userModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Suppiers ConvertUserModelToUser(UserModel userModel)
        {
            try
            {
                Suppiers suppier = new Suppiers
                {
                    SuppierId = userModel.UserId,
                    Name = userModel.Name,
                    Email = userModel.Email,
                    Password = userModel.Password
                };

                return suppier != null ? suppier : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
