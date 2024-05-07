using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Helper.Helper;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;
using Taskmanagement_Repository.Interface;

namespace Taskmanagement_Repository.Service
{
    public class CityService : ICityRepository
    {
        private readonly TaskManagement_557Entities _context;

        public CityService()
        {
            _context = new TaskManagement_557Entities();
        }
        public List<CityModel> GetCityByStateId(int id)
        {
            try
            {
                List<CityModel> cityModelList = new List<CityModel>();
                List<City> cityList = _context.City.Where(m => m.StateID == id).ToList();

                cityModelList = CityHelper.ConvertCityToCityModel(cityList);
                return cityModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
