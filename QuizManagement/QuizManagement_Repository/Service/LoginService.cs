using QuizManagement_Helper.ModelHelper;
using QuizManagement_Model.DbContext;
using QuizManagement_Model.ViewModel;
using QuizManagement_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Repository.Service
{
    public class LoginService : ILoginRepository
    {
        private readonly Quizmanagement_557Entities _context;
        public LoginService()
        {
            _context = new Quizmanagement_557Entities();
        }

        public bool AddRegister(UserModel registerModel)
        {
            try
            {
                int isCheckingSaveOrNot = 0;
                Users user = new Users();
                user = LoginHelper.ConvertUserModelToUser(registerModel);

                if(!_context.Users.Any(m => m.email == user.email))
                {
                    _context.Users.Add(user);
                    isCheckingSaveOrNot = _context.SaveChanges();
                }

                return isCheckingSaveOrNot > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
