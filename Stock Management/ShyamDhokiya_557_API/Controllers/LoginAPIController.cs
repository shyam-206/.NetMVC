using ShyamDhokiya_557_API.JWTAuthencation;
using ShyamDhokiya_557_Model.ViewModel;
using ShyamDhokiya_557_Repository.Repository;
using ShyamDhokiya_557_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShyamDhokiya_557_API.Controllers
{
    public class LoginAPIController : ApiController
    {
        private readonly LoginRepository repo = new LoginService();

        [Route("api/LoginAPI/AddRegister")]
        [HttpPost]
        public bool AddRegister(UserModel userModel)
        {
            try
            {
                bool savRegister = repo.AddRegister(userModel);
                return savRegister;
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }

        [Route("api/LoginAPI/IsUserExist")]
        [HttpPost]
        public UserModel IsUserExist(LoginModel loginModel)
        {
            try
            {
                UserModel userModel = repo.IsUserExist(loginModel);
                if(userModel != null && userModel.UserId > 0)
                { 
                    userModel.Token = Authencation.GenerateJWTAuthetication(userModel.Email, "Suppier");
                }
                return userModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/LoginAPI/IsAdminExist")]
        [HttpPost]
        public UserModel IsAdminExist(LoginModel loginModel)
        {

            try
            {
                UserModel userModel = repo.IsAdminExist(loginModel);
                if(userModel != null && userModel.UserId > 0)
                {
                    userModel.Token = Authencation.GenerateJWTAuthetication(userModel.Email, "SuperAdmin");
                }
                return userModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}