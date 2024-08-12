using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Repository.Repository
{
    public interface UserRepository
    {
        BikeModel GetBikeById(int BikeId);
        List<BikeModel> BikeList(int UserId);
        FavoriteToggleResult ToggleFavourite(int BikeId, int UserId);
        UserModel GetUserProfile(int UserId);
        bool EditUserProfile(UserModel userModel);
        List<CountryModel> GetCountryList();
        List<StateModel> GetStateById(int CountryId);
        List<CityModel> GetCityById(int StateId);
    }
}
