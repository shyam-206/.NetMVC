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
    public class LoginService : LoginRepository
    {
        private readonly ShyamDhokiya_557Entities db = new ShyamDhokiya_557Entities();
        public bool AddRegsiter(UserModel userModel)
        {
            int UserId = 0;
            if (!db.Users.Any(m => m.Email == userModel.Email))
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
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
                    new SqlParameter("@Image",userModel.Image),
                    new SqlParameter("@Password",userModel.Password),
                };

                UserId = db.Database.SqlQuery<int>("exec InsertRegisterData @FirstName, @LastName, @MiddleName, @Email, @MobileNumber,@DOB, @Address1, @Address2, @CountryId, @StateId, @CityId,@ZipCode,@Image,@Password", sqlParameters).FirstOrDefault();

            }
            return UserId > 0 ? true : false; 
        }

        public AdminModel IsAdminExist(LoginModel loginModel)
        {
            Admin admin = db.Admin.Where(m => m.Email == loginModel.Email && m.Password == loginModel.Password).FirstOrDefault();
            AdminModel adminModel = new AdminModel();
            if (admin != null && admin.UserId > 0)
            {
                adminModel.UserId = admin.UserId;
                adminModel.Email = admin.Email;
                adminModel.Password = admin.Password;
            }

            return adminModel;

        }

        public UserModel IsUserExist(LoginModel loginModel)
        {

            Users users = db.Users.Where(m => m.Email == loginModel.Email && m.Password == loginModel.Password).FirstOrDefault();

            UserModel userModel = new UserModel();
            if (users != null && users.UserId > 0)
            {
                userModel.UserId = users.UserId;
                userModel.Email = users.Email;
                userModel.Password = users.Password;
                userModel.Image = users.Image;
            }

            return userModel;

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
    }
}
