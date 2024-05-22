using QuizManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Repository.Interface
{
    public interface ILoginRepository
    {
        bool AddRegister(UserModel registerModel);
    }
}
