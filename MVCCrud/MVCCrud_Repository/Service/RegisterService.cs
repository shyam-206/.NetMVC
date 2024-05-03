using MVCCrud_Helper.Helper;
using MVCCrud_Model.Context;
using MVCCrud_Model.CustomModel;
using MVCCrud_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCCrud_Repository.Service
{
    public class RegisterService : IRegister
    {

        private readonly shyamEntities _context;

        public RegisterService()
        {
            _context = new shyamEntities();
        }
        public RegisterModel AddRegister(RegisterModel newUserModel)
        {
            try
            {

                RegisterUser addUser = new RegisterUser();
                addUser = RegisterHelper.ConvertRegisterModelToRegisterUser(newUserModel);
                RegisterModel registerModel = new RegisterModel();
                var isEmailExist = _context.RegisterUser.Any(m => m.UserEmail == addUser.UserEmail);

                if (!isEmailExist)
                {
                    registerModel = RegisterHelper.ConvertToRegisterUserToRegisterModel(addUser);
                    _context.RegisterUser.Add(addUser);
                    _context.SaveChanges();
                    return registerModel;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool CheckingUserExist(RegisterModel loginUserModel)
        {
            try
            {
                RegisterUser register = RegisterHelper.ConvertRegisterModelToRegisterUser(loginUserModel);

                //for the checking the user is exist or not
                RegisterUser isUserExist = _context.RegisterUser.FirstOrDefault(m => m.UserName == register.UserName && m.Password == register.Password);

                if (isUserExist != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<RegisterModel> GetAllRegisterUserList()
        {
            try
            {
                List<RegisterUser> registerUserList = _context.RegisterUser.ToList();
                List<RegisterModel> registerUserModelList = RegisterHelper.GetRegsiterModelByHelper(registerUserList);
                return registerUserModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
