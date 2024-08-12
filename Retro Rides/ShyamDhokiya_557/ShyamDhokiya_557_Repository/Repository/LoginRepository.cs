using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Repository.Repository
{
    public interface LoginRepository
    {
        bool AddRegsiter(UserModel userModel);

        UserModel IsUserExist(LoginModel loginModel);

        AdminModel IsAdminExist(LoginModel loginModel);
        List<CountryModel> GetCountryList();
        List<StateModel> GetStateById(int CountryId);
        List<CityModel> GetCityById(int StateId);
    }
}
