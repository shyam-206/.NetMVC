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
    public class StudentAPIController : ApiController
    {
        private readonly IStudentRepository studentRepository;
        private readonly ITaskRepository taskRepository;

        public StudentAPIController()
        {
            studentRepository = new StudentService();
            taskRepository = new TaskService();
        }


        [Route("api/StudentAPI/GetAllTaskAssignByTeacher")]
        public List<AssignmentModelList> GetAllTaskAssignByTeacher(int studentId)
        {
            try
            {
                List<AssignmentModelList> assignments = studentRepository.GetAllTaskAssignByTeacher(studentId);
                return assignments != null ? assignments : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/StudentAPI/CompleteList")]
        public List<AssignmentModelList> GetCompleteTaskList(int studentId)
        {
            try
            {
                List<AssignmentModelList> CompleteList = new List<AssignmentModelList>();
                CompleteList = studentRepository.GetCompleteTaskList(studentId);
                return CompleteList != null ? CompleteList : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/StudentAPI/PendingList")]
        public List<AssignmentModelList> GetPendingTaskList(int studentId)
        {
            try
            {
                List<AssignmentModelList> PendingList = new List<AssignmentModelList>();
                PendingList = studentRepository.GetPendingTaskList(studentId);
                return PendingList != null ? PendingList : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/StudentAPI/GetTaskByTaskId")]
        public TaskModel GetTaskByTaskId(int TaskId)
        {
            try
            {
                TaskModel taskModel = new TaskModel();
                taskModel = taskRepository.GetTaskByTaskId(TaskId);
                return taskModel != null ? taskModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("api/StudentAPI/GetTaskAssignByTeacherCount")]
        public int GetTaskAssignByTeacherCount(int studentId)
        {
            try
            {
                int taskCount = 0;
                taskCount = studentRepository.GetTaskAssignByTeacherCount(studentId);
                return taskCount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("api/StudentAPI/GetCompleteTaskCount")]
        public int GetCompleteTaskCount(int studentId)
        {
            try
            {
                int taskCount = 0;
                taskCount = studentRepository.GetCompleteTaskCount(studentId);
                return taskCount;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Route("api/StudentAPI/GetPendingTaskCount")]
        public int GetPendingTaskCount(int studentId)
        {
            try
            {
                int taskCount = 0;
                taskCount = studentRepository.GetPendingTaskCount(studentId);
                return taskCount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("api/StudentAPI/SetAssignmentStatus")]
        public bool SetAssignmentStatus(int id)
        {
            try
            {
                bool setStatus = studentRepository.AssignmentStatusUpdate(id);
                return setStatus;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}