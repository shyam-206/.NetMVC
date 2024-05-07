using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.ViewModel;

namespace Taskmanagement_Repository.Interface
{
    public interface ITeacherRepository
    {
        bool AddTeacher(RegisterModel registerModel);
    }
}
