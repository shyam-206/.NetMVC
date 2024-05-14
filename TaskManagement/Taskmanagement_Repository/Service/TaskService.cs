using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement_Helper.Helper;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;
using Taskmanagement_Repository.Interface;
using System.Data.SqlClient;

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
                    assignment.Status = Convert.ToBoolean(0);
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

        public List<StudentModel> GetAllStudentList(int Taskid)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@id",Taskid)
                };
                List<StudentModel> studentModelList = new List<StudentModel>();
                studentModelList = _context.Database.SqlQuery<StudentModel>("exec GetAllStudentList @id", sqlParameters).ToList();
                return studentModelList;
             }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TaskModel> GetAllTaskList(int teacherId)
        {
            try
            {
                List<TaskModel> taskModelList = new List<TaskModel>();
                List<Task> taskList = _context.Task.Where(m => m.CreatorID == teacherId).ToList();

                taskModelList = TaskHelper.ConvertTaskListToTaskModelList(taskList);
                return taskModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetTotaTaskCount(int teacherId)
        {
            try
            {
                int TotalTaskcount = 0;
                List<Task> taskList = _context.Task.Where(m => m.CreatorID == teacherId).ToList();
                if(taskList != null)
                {
                    TotalTaskcount = taskList.Count();
                }
                return TotalTaskcount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetAllAssignTaskCount(int teacherId)
        {
            try
            {
                int AssignTaskToStudentCount = 0;
                List<Assignment> assignTaskList = _context.Assignment.Where(m => m.Task.CreatorID == teacherId).ToList();

                if(assignTaskList != null)
                {
                    AssignTaskToStudentCount = assignTaskList.Count();
                }
                return AssignTaskToStudentCount;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int CompletedTaskCount(int teacherId)
        {
            try
            {
                int completedTaskCount = 0;
                //true mean they will submit task
                List<Assignment> assignTaskList = _context.Assignment.Where(m => m.Task.CreatorID == teacherId && m.Status == true).ToList();

                if (assignTaskList != null)
                {
                    completedTaskCount = assignTaskList.Count();
                }
                return completedTaskCount;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int PendingTaskCount(int teacherId)
        {
            try
            {
                int pendingTaskCount = 0;
                List<Assignment> pendingTaskList = _context.Assignment.Where(m => m.Task.CreatorID == teacherId && m.Status == false).ToList();

                if(pendingTaskList != null)
                {
                    pendingTaskCount = pendingTaskList.Count();
                }
                return pendingTaskCount;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TaskModel GetTaskByTaskId(int TaskID)
        {
            try
            {
                TaskModel taskModel = new TaskModel();
                Task task = new Task();

                task = _context.Task.FirstOrDefault(m => m.TaskID == TaskID);
                taskModel = TaskHelper.ConvertTasktoTaskModel(task);

                return taskModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Assignment> CompleteTaskList(int teacherId)
        {
            try
            {
                
                List<Assignment> CompleteTaskList = new List<Assignment>();

                CompleteTaskList = _context.Assignment.Where(m => m.Task.CreatorID == teacherId && m.Status == true).ToList();
                
                if(CompleteTaskList != null)
                {
                    return CompleteTaskList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
