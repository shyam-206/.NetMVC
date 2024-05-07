using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;
using Taskmanagement_Repository.Interface;

namespace Taskmanagement_Repository.Service
{
    public class StudentService : IStudentRepository
    {

        private readonly TaskManagement_557Entities _context;

        public StudentService()
        {
            _context = new TaskManagement_557Entities();
        }
        public bool AddStudent(RegisterModel registerModel)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
