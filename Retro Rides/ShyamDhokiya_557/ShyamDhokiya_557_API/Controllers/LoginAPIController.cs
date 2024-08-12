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

        [Route("api/Login/AddRegsiter")]
        public bool AddRegsiter(UserModel userModel)
        {
            return repo.AddRegsiter(userModel);
        }

        [Route("api/Login/IsAdminExist")]
        public AdminModel IsAdminExist(LoginModel loginModel)
        {
            AdminModel adminModel = repo.IsAdminExist(loginModel);
            if(adminModel != null && adminModel.UserId > 0)
            {
                adminModel.Token = JWTAuthencation.Authencation.GenerateJWTAuthetication(adminModel.Email, "Admin");
            }
            return adminModel;
        }
        [Route("api/Login/IsUserExist")]

        public UserModel IsUserExist(LoginModel loginModel)
        {
            UserModel userModel = repo.IsUserExist(loginModel);
            if(userModel != null && userModel.UserId > 0)
            {
                userModel.Token = JWTAuthencation.Authencation.GenerateJWTAuthetication(userModel.Email, "User");
            }

            return userModel;
        }

        [Route("api/Login/GetCityById")]
        public List<CityModel> GetCityById(int StateId)
        {
            return repo.GetCityById(StateId);
        }

        [Route("api/Login/GetCountryList")]
        public List<CountryModel> GetCountryList()
        {
            return repo.GetCountryList();
        }

        [Route("api/Login/GetstateById")]
        public List<StateModel> GetstateById(int CountryId)
        {
            return repo.GetStateById(CountryId);
        }
    }
}