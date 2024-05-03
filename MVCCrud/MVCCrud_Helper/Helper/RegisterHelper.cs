using MVCCrud_Model.Context;
using MVCCrud_Model.CustomModel;
using System;
using System.Collections.Generic;

namespace MVCCrud_Helper.Helper
{
    public class RegisterHelper
    {
        public static List<RegisterModel> GetRegsiterModelByHelper(List<RegisterUser> registerUserList)
        {
            try
            {
                List<RegisterModel> registerModelList = new List<RegisterModel>();
                if (registerUserList != null)
                {
                    foreach (var register in registerUserList)
                    {
                        RegisterModel registerModel = new RegisterModel();
                        registerModel.UserID = register.UserID;
                        registerModel.UserName = register.UserName;
                        registerModel.FullName = register.FullName;
                        registerModel.UserEmail = register.UserEmail;
                        registerModel.Password = register.Password;
                        registerModelList.Add(registerModel);
                    }
                }
                return registerModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static RegisterUser ConvertRegisterModelToRegisterUser(RegisterModel newUserModel)
        {
            try
            {
                RegisterUser _registerUser = new RegisterUser();

                if (newUserModel != null)
                {
                    _registerUser.UserID = newUserModel.UserID;
                    _registerUser.UserName = newUserModel.UserName;
                    _registerUser.FullName = newUserModel.FullName;
                    _registerUser.UserEmail = newUserModel.UserEmail;
                    _registerUser.Password = newUserModel.Password;
                }
                return _registerUser;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static RegisterModel ConvertToRegisterUserToRegisterModel(RegisterUser registerUser)
        {
            try
            {
                RegisterModel registerModel = new RegisterModel();
                if (registerUser != null)
                {
                    registerModel.UserID = registerUser.UserID;
                    registerModel.UserName = registerUser.UserName;
                    registerModel.FullName = registerUser.FullName;
                    registerModel.UserEmail = registerUser.UserEmail;
                    registerModel.Password = registerUser.Password;
                }

                return registerModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
