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
        bool AddRegister(UserModel userModel);

        UserModel IsUserExist(LoginModel loginModel);

        UserModel IsAdminExist(LoginModel loginModel);
    }
}
