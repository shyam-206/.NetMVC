using QuizManagement_Model.ViewModel;
using QuizManagement_Repository.Interface;
using QuizManagement_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizManagementAPI.Controllers
{
    public class LoginAPIController : ApiController
    {
        private readonly ILoginRepository loginRepository;
        public LoginAPIController()
        {
            loginRepository = new LoginService();
        }
        [HttpPost]
        [Route("api/LoginAPI/CheckAdminExist")]
        public AdminModel CheckAdminExist(LoginModel loginModel)
        {
            try
            {
                AdminModel adminModel = loginRepository.CheckAdminExist(loginModel);
                return adminModel != null ? adminModel : null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("api/LoginAPI/CheckUserExist")]
        public UserModel CheckUserExist(LoginModel loginModel)
        {
            try
            {
                UserModel userModel = loginRepository.CheckUserExist(loginModel);
                return userModel != null ? userModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("api/LoginAPI/AddRegister")]
        public bool AddRegister(UserModel userModel)
        {
            try
            {
                bool isAddUser = loginRepository.AddRegister(userModel);
                return isAddUser;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}