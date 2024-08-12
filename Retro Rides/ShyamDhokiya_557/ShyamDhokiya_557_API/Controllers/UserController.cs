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
    public class UserController : ApiController
    {

        private readonly UserRepository repo = new UserService();

        [HttpGet]
        [Route("api/User/GetBikeById")]
        public BikeModel GetBikeById(int BikeId)
        {
            return repo.GetBikeById(BikeId);
        }

        [HttpGet]
        [Route("api/User/ToggleFavourite")]
        public FavoriteToggleResult ToggleFavourite(int BikeId, int UserId)
        {
            return repo.ToggleFavourite(BikeId,UserId);
        }

        [HttpGet]
        [Route("api/User/BikeList")]
        public List<BikeModel> BikeList(int UserId)
        {
            return repo.BikeList(UserId);
        }

        [HttpGet]
        [Route("api/User/GetUserProfile")]
        public UserModel GetUserProfile(int UserId)
        {
            return repo.GetUserProfile(UserId);
        }

        [Route("api/User/GetCityById")]
        public List<CityModel> GetCityById(int StateId)
        {
            return repo.GetCityById(StateId);
        }

        [Route("api/User/GetCountryList")]
        public List<CountryModel> GetCountryList()
        {
            return repo.GetCountryList();
        }

        [Route("api/User/GetstateById")]
        public List<StateModel> GetstateById(int CountryId)
        {
            return repo.GetStateById(CountryId);
        }

        [Route("api/User/EditUserProfile")]
        public bool EditUserProfile(UserModel userModel)
        {
            return repo.EditUserProfile(userModel);
        }
    }
}