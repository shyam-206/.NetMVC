using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement_Helper.Helper;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;
using Taskmanagement_Repository.Interface;

namespace Taskmanagement_Repository.Service
{
    public class TaskService : ITaskRepository
    {
        private readonly TaskManagement_557Entities _context;

        public TaskService()
        {
            _context = new TaskManagement_557Entities();
        }

        public bool AddTask(TaskModel taskModel)
        {
            try
            {
                int isCheckingSaveOrNot = 0;
                Task task = new Task();
                task = TaskHelper.ConvertTaskModeltoTask(taskModel);
               _context.Task.Add(task);
                isCheckingSaveOrNot = _context.SaveChanges();

                if(isCheckingSaveOrNot > 0)
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

        public bool AssignTask(AssignmentModel assignmentModel)
        {
            try
            {
                Assignment assignment = new Assignment();
                assignment.TaskID = assignmentModel.TaskID;

                foreach(var item in assignmentModel.StudentID)
                {
                    int isCheckingSaveOrNot = 0;
                    assignment.StudentID = ConvertStringToInt(item);
                    _context.Assignment.Add(assignment);
                    isCheckingSaveOrNot = _context.SaveChanges();

                    if(isCheckingSaveOrNot <= 0)
                    {
                        return false;
                    }

                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private int? ConvertStringToInt(string item)
        {
            try
            {
                return Convert.ToInt32(item);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<StudentModel> GetAllStudentList()
        {
            try
            {
                List<StudentModel> studentModelList = new List<StudentModel>();
                List<Student> studentList = new List<Student>();
                studentList = _context.Student.ToList();
                studentModelList = StudentHelper.ConvertStudentToStudentModel(studentList);
                return studentModelList;
             }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TaskModel> GetAllTaskList()
        {
            try
            {
                List<TaskModel> taskModelList = new List<TaskModel>();
                List<Task> taskList = _context.Task.ToList();

                taskModelList = TaskHelper.ConvertTaskToTaskModel(taskList);
                return taskModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
