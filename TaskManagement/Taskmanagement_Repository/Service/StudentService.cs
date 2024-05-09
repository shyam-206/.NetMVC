using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement_Helper.Helper;
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
                int isCheckingSaveOrNot = 0;
                Student student = new Student();
                student = StudentHelper.ConvertRegisterModelToStudent(registerModel);

                if (!_context.Student.Any(m => m.Email == student.Email))
                {
                    _context.Student.Add(student);
                    isCheckingSaveOrNot = _context.SaveChanges();
                }

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

        public bool AssignmentStatusUpdate(int id)
        {
            try
            {
                Assignment assignments = new Assignment();
                assignments = _context.Assignment.FirstOrDefault(m => m.AssignmentID == id);

                int isUpdateSaveOrNot = 0;
                if(assignments.Status != Convert.ToBoolean(1))
                {
                    assignments.Status = Convert.ToBoolean(1);
                    _context.Entry(assignments).State = EntityState.Modified;
                    isUpdateSaveOrNot = _context.SaveChanges();
                }

                if(isUpdateSaveOrNot > 0)
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

        public List<Assignment> GetAllTaskAssignByTeacher(int StudentId)
        {
            try
            {
                List<TaskModel> taskModels = new List<TaskModel>();
                Student student = new Student();
                student = _context.Student.FirstOrDefault(m => m.StudentID == StudentId);
                List<Assignment> assignmentList = _context.Assignment.Where(m => m.StudentID == student.StudentID).ToList();
                return assignmentList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCompleteTaskCount(int studentId)
        {
            try
            {
                int completeTaskCount = 0;
                List<Assignment> CompleteAssignmentList = new List<Assignment>();
                CompleteAssignmentList = _context.Assignment.Where(m => m.StudentID == studentId && m.Status == true).ToList();

                if(CompleteAssignmentList != null)
                {
                    completeTaskCount = CompleteAssignmentList.Count();
                }

                return completeTaskCount;
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }

        public int GetPendingTaskCount(int studentId)
        {
            try
            {
                int pendingTaskCount = 0;
                List<Assignment> pendingAssignmentList = new List<Assignment>();
                pendingAssignmentList = _context.Assignment.Where(m => m.StudentID == studentId && m.Status == false).ToList();

                if (pendingAssignmentList != null)
                {
                    pendingTaskCount = pendingAssignmentList.Count();
                }

                return pendingTaskCount;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetTaskAssignByTeacherCount(int StudentId)
        {
            try
            {
                int TotalAssignTaskCount = 0;
                List<Assignment> assignments = new List<Assignment>();
                assignments = _context.Assignment.Where(m => m.StudentID == StudentId).ToList();

                if(assignments != null)
                {
                    TotalAssignTaskCount = assignments.Count();
                }

                return TotalAssignTaskCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }         
        }
    }
}
