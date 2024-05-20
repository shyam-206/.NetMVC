using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagement_Model.ViewModel;
using Taskmanagement_Repository.Interface;
using Taskmanagement_Repository.Service;

namespace TaskManagementAPI.Controllers
{
    public class TeacherAPIController : ApiController
    {
        private readonly ITaskRepository taskRepository;

        public TeacherAPIController()
        {
            taskRepository = new TaskService();
        }

        [HttpGet]
        [Route("api/TeacherAPI/CompleteTaskList")]
        public List<AssignmentModelList> CompleteTaskList(int teacherId)
        {
            try
            {
                List<AssignmentModelList> CompleteTaskList = new List<AssignmentModelList>();
                CompleteTaskList = taskRepository.CompleteTaskList(teacherId);
                if (CompleteTaskList != null)
                {
                    return CompleteTaskList;
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

        [HttpGet]
        [Route("api/TeacherAPI/GetAllTaskList")]
        public List<TaskModel> GetAllTaskList(int teacherId)
        {
            try
            {
                List<TaskModel> taskList = taskRepository.GetAllTaskList(teacherId);
                return taskList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("api/TeacherAPI/AssignTaskList")]
        public List<AssignmentModelList> AssignTaskList(int teacherId)
        {
            try
            {
                List<AssignmentModelList> AssignTaskList = new List<AssignmentModelList>();
                AssignTaskList = taskRepository.AssignTaskList(teacherId);
                if (AssignTaskList != null)
                {
                    return AssignTaskList;
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

        [HttpGet]
        [Route("api/TeacherAPI/PendingTaskList")]
        public List<AssignmentModelList> PendingTaskList(int teacherId)
        {
            try
            {
                List<AssignmentModelList> PendingTaskModelList = taskRepository.PendingTaskList(teacherId);
                if(PendingTaskModelList != null)
                {
                    return PendingTaskModelList;
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

        [HttpPost]
        [Route("api/TeacherAPI/AssignTask")]
        public bool AssignTask(AssignmentModel assignment)
        {
            try
            {
                bool checkSaveOrNot = taskRepository.AssignTask(assignment);
                return checkSaveOrNot;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("api/TeacherAPI/AddTask")]
        public bool AddTask(TaskModel taskModel)
        {
            try
            {
                bool checkSaveOrNot = taskRepository.AddTask(taskModel);
                return checkSaveOrNot;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("api/TeacherAPI/GetTotaTaskCount")]
        public int GetTotaTaskCount(int teacherId)
        {
            try
            {
                int TotalTaskcount = 0;
                TotalTaskcount = taskRepository.GetTotaTaskCount(teacherId);
                return TotalTaskcount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("api/TeacherAPI/GetAllAssignTaskCount")]
        public int GetAllAssignTaskCount(int teacherId)
        {
            try
            {
                int AssignTaskCount = 0;
                AssignTaskCount = taskRepository.GetAllAssignTaskCount(teacherId);
                return AssignTaskCount;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("api/TeacherAPI/CompletedTaskCount")]
        public int CompletedTaskCount(int teacherId)
        {
            try
            {
                int CompletedTaskCount = 0;
                CompletedTaskCount = taskRepository.CompletedTaskCount(teacherId);
                return CompletedTaskCount;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("api/TeacherAPI/PendingTaskCount")]
        public int PendingTaskCount(int teacherId)
        {
            try
            {
                int PendingTaskCount = 0;
                PendingTaskCount = taskRepository.PendingTaskCount(teacherId);
                return PendingTaskCount;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("api/TeacherAPI/GetAllStudentList")]
        public List<StudentModel> GetAllStudentList(int Taskid)
        {
            try
            {
                List<StudentModel> studentModelList = new List<StudentModel>();
                studentModelList = taskRepository.GetAllStudentList(Taskid);
                return studentModelList != null ? studentModelList : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}