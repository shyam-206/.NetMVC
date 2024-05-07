using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;

namespace TaskManagement_Helper.Helper
{
    public class CityHelper
    {
        public static List<CityModel> ConvertCityToCityModel(List<City> cityList)
        {
            try
            {
                List<CityModel> cityModelList = new List<CityModel>();
                if(cityList != null)
                {
                    foreach (var city in cityList)
                    {
                        CityModel cityModel = new CityModel();
                        cityModel.CityID = city.CityID;
                        cityModel.CityName = city.CityName;
                        cityModel.StateID = city.StateID;
                        cityModelList.Add(cityModel);
                    }
                }

                return cityModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
    }
}
