using ShyamDhokiya_557_Model.DbContext;
using ShyamDhokiya_557_Model.ViewModel;
using ShyamDhokiya_557_Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShyamDhokiya_557_Repository.Service
{
    public class UserService : UserRepository
    {
        private readonly ShyamDhokiya_557Entities db = new ShyamDhokiya_557Entities();

        public List<BikeModel> BikeList(int UserId)
        {

            List<BikeModel> list = new List<BikeModel>();
            list = db.Database.SqlQuery<BikeModel>("exec GetBikeList").ToList();
            foreach(var item in list)
            {
                item.IsFavourite = db.Favourites.Any(m => m.BikeId == item.BikeId && m.UserId == UserId);
            }
            return list;
        }

        public BikeModel GetBikeById(int BikeId)
        {
            BikeModel bikeModel = new BikeModel();
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@BikeId",BikeId)
            };

            bikeModel = db.Database.SqlQuery<BikeModel>("exec GetBikeById @BikeId", sqlParameters).FirstOrDefault();
            return bikeModel;
        }

        public UserModel GetUserProfile(int UserId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@UserId",UserId)
            };
            UserModel userModel = db.Database.SqlQuery<UserModel>("exec GetUserProfile @UserId", sqlParameters).FirstOrDefault();

            return userModel;
        }

        public FavoriteToggleResult ToggleFavourite(int BikeId, int UserId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@BikeId",BikeId),
                new SqlParameter("@UserId",UserId),
            };

            FavoriteToggleResult result = db.Database.SqlQuery<FavoriteToggleResult>("exec ToggleFavourite @UserId, @BikeId ", sqlParameters).FirstOrDefault();

            return result;
        }

        public List<CityModel> GetCityById(int StateId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@StateId",StateId)
            };
            List<CityModel> cities = db.Database.SqlQuery<CityModel>("exec GetCityById @StateId", sqlParameters).ToList();
            return cities;
        }

        public List<CountryModel> GetCountryList()
        {
            List<CountryModel> countryModels = db.Database.SqlQuery<CountryModel>("exec GetCountryList").ToList();
            return countryModels;
        }

        public List<StateModel> GetStateById(int CountryId)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                        new SqlParameter("@CountryId",CountryId)
                };
                List<StateModel> stateModels = db.Database.SqlQuery<StateModel>("exec GetstateById @CountryId", sqlParameters).ToList();
                return stateModels;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EditUserProfile(UserModel userModel)
        {
            int editId = 0;
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@UserId",userModel.UserId),
                new SqlParameter("@FirstName",userModel.FirstName),
                new SqlParameter("@LastName",userModel.LastName),
                new SqlParameter("@MiddleName",(object)userModel.MiddleName ?? DBNull.Value),
                new SqlParameter("@Email",userModel.Email),
                new SqlParameter("@MobileNumber",userModel.MobileNumber),
                new SqlParameter("@DOB",(object)userModel.DOB ?? DBNull.Value),
                new SqlParameter("@Address1",userModel.Address1),
                new SqlParameter("@Address2",(object)userModel.Address2 ?? DBNull.Value),
                new SqlParameter("@CountryId",Convert.ToString(userModel.CountryId)),
                new SqlParameter("@StateId",Convert.ToString(userModel.StateId)),
                new SqlParameter("@CityId",Convert.ToString( userModel.CityId)),
                new SqlParameter("@ZipCode",userModel.ZipCode),
                new SqlParameter("@Image",(object)userModel.Image ?? DBNull.Value)
            };

            editId = db.Database.SqlQuery<int>("exec EditUserProfile  @UserId,@FirstName, @LastName, @MiddleName, @Email, @MobileNumber,@DOB, @Address1, @Address2, @CountryId, @StateId, @CityId,@ZipCode,@Image", sqlParameters).FirstOrDefault();

            return editId > 0 ? true : false;
        }
    }
}
