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
    public class TeacherService : ITeacherRepository
    {
        private readonly TaskManagement_557Entities _context;

        public TeacherService()
        {
            _context = new TaskManagement_557Entities();
        }

        public bool AddTeacher(RegisterModel registerModel)
        {
            try
            {
                int isCheckingSaveOrNot = 0;
                Teacher teacher = new Teacher();
                teacher = TeacherHelper.ConvertRegisterModelToTeacher(registerModel);
                _context.Teacher.Add(teacher);
                isCheckingSaveOrNot = _context.SaveChanges();

                if (isCheckingSaveOrNot > 0)
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
    }
}
