using MVCCrud_Model.CustomModel;
using System.Collections.Generic;

namespace MVCCrud_Repository.Interface
{
    public interface IRegister
    {
        List<RegisterModel> GetAllRegisterUserList();

        RegisterModel AddRegister(RegisterModel newUser);

        bool CheckingUserExist(RegisterModel loginUserModel);


    }
}
