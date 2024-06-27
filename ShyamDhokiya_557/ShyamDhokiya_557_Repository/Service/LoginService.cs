using ShyamDhokiya_557_Helper.Helper;
using ShyamDhokiya_557_Model.DbContext;
using ShyamDhokiya_557_Model.ViewModel;
using ShyamDhokiya_557_Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Repository.Service
{
    public class LoginService : LoginRepository
    {
        private readonly StockManagement_557Entities db = new StockManagement_557Entities();

        public bool AddRegister(UserModel userModel)
        {
            try
            {
                int SaveRegsiter = 0;
                Suppiers suppier = UserHelper.ConvertUserModelToUser(userModel);

                if(!db.Suppiers.Any(m => m.Email == suppier.Email))
                {
                    Suppiers newRegister = db.Suppiers.Add(suppier);
                    SaveRegsiter = db.SaveChanges();

                    Cart cart = new Cart();
                    cart.SuppierId = newRegister.SuppierId;
                    db.Cart.Add(cart);
                    db.SaveChanges();
                }

                return SaveRegsiter > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UserModel IsAdminExist(LoginModel loginModel)
        {
            try
            {
                UserModel newLogin = new UserModel();
                AdminStock adminStock = db.AdminStock.Where(m => m.AdminEmail == loginModel.Email && m.AdminPassword == loginModel.Password).FirstOrDefault();

                if(adminStock != null && adminStock.AdminId > 0)
                {
                    newLogin.UserId = adminStock.AdminId;
                    newLogin.Email = adminStock.AdminEmail;
                    newLogin.Name = adminStock.AdminName;
                    newLogin.Password = adminStock.AdminPassword;
                }

                return newLogin != null ? newLogin : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UserModel IsUserExist(LoginModel loginModel)
        {
            try
            {
                UserModel newLogin = new UserModel();
                Suppiers suppier = db.Suppiers.Where(m => m.Email == loginModel.Email && m.Password == loginModel.Password).FirstOrDefault();

                if (suppier != null && suppier.SuppierId > 0)
                {
                    newLogin.UserId = suppier.SuppierId;
                    newLogin.Email = suppier.Email;
                    newLogin.Name = suppier.Name;
                    newLogin.Password = suppier.Password;
                }

                return newLogin != null ? newLogin : null;
            }
            catch (Exception ex) 
            {

                throw ex;
            }
        }
    }
}
