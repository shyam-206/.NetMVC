using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskManagement.Common;
using TaskManagement.CustomFilter;
using TaskManagement.Session;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;
using Taskmanagement_Repository.Interface;
using Taskmanagement_Repository.Service;

namespace TaskManagement.Controllers
{
    [CustomAuthorization]
    [CustomErrorMessage]
    public class TeacherController : Controller
    {
        private readonly ITaskRepository taskRepository;
    
        public TeacherController()
        {
            taskRepository = new TaskService();
        }
        // GET: Teacher
        public ActionResult Index()
        {
            try
            {
                int teacherId = SessionHelper.UserId;
                ViewBag.TotalTaskCount = taskRepository.GetTotaTaskCount(teacherId);
                ViewBag.AssignTaskCount = taskRepository.GetAllAssignTaskCount(teacherId);
                ViewBag.CompletedTaskCount = taskRepository.CompletedTaskCount(teacherId);
                ViewBag.PendingTaskCount = taskRepository.PendingTaskCount(teacherId);
                return View();
            }
            catch (Exception ex)
            {

                    throw ex;
            }
        }

        [HttpGet]
        public ActionResult Addtask()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Addtask(TaskModel taskModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    /*bool isCheckingSaveOrNot = taskRepository.AddTask(taskModel);*/
                    string url = "api/TeacherAPI/AddTask";
                    string jsonContent = JsonConvert.SerializeObject(taskModel);
                    string response = await WebHelper.HttpClientPostRequest(url, jsonContent);
                    bool CheckSaveOrNot = JsonConvert.DeserializeObject<bool>(response);
                    if (CheckSaveOrNot)
                    {
                        TempData["Addtask"] = "New Task Added";
                        return RedirectToAction("Index", "Teacher");
                    }
                }
                return View(taskModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AssignTaskByTeacher()
        {
            try
            {
                try
                {
                    List<TaskModel> taskModelList = taskRepository.GetAllTaskList(SessionHelper.UserId);
                    return View(taskModelList);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult AssignTask(int TaskId)
        {
            try
            {
                ViewBag.TaskId = TaskId;
                ViewBag.AllStudentList = taskRepository.GetAllStudentList(TaskId);
                AssignmentModel assignmentModel = new AssignmentModel();    
                return PartialView("AssignTask", assignmentModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AssignTask([Bind(Exclude = "Status")] AssignmentModel assignmentModel)
        {
            try
            {
                int TaskId = (int)assignmentModel.TaskID;
                if(ModelState.IsValid) {

                    string url = "api/TeacherAPI/AssignTask";
                    string jsonContent = JsonConvert.SerializeObject(assignmentModel);
                    string response = await WebHelper.HttpClientPostRequest(url, jsonContent);
                    bool CheckSaveOrNot = JsonConvert.DeserializeObject<bool>(response);

                    /*bool CheckSaveOrNot = taskRepository.AssignTask(assignmentModel);*/
                    if (CheckSaveOrNot)
                    {
                        TempData["Assgintask"] = "Task was assign to student";
                        return RedirectToAction("Index","Teacher");
                    }
                }
                ViewBag.AllStudentList = taskRepository.GetAllStudentList(TaskId);
                return View(assignmentModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> Tasklist(int index = 1)
        {
            try
            {
                /*List<TaskModel> taskModel = taskRepository.GetAllTaskList(SessionHelper.UserId);*/
                string url = $"api/TeacherAPI/GetAllTaskList?teacherId={SessionHelper.UserId}";
                int currnetIndex = index;
                int count = 2;
                string response = await WebHelper.HttpRequestResponce(url);
                List<TaskModel> taskModel = JsonConvert.DeserializeObject<List<TaskModel>>(response);
                List<TaskModel> taskModelList = taskModel.Skip((currnetIndex - 1) * count).Take(count).ToList();

                PaginationModel paginationModel = new PaginationModel();
                paginationModel.TaskList = taskModelList;
                paginationModel.CurrentIndex = currnetIndex;
                paginationModel.Count = taskModel.Count();

                return View(paginationModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> CompleteTaskList()
        {
            try
            {
                /*completeTaskList = taskRepository.CompleteTaskList(SessionHelper.UserId);*/
                List<AssignmentModelList> completeTaskList = new List<AssignmentModelList>();
                int teacherId = SessionHelper.UserId;
                string url = $"api/TeacherAPI/CompleteTaskList?teacherId={teacherId}";
                string response = await WebHelper.HttpRequestResponce(url);
                completeTaskList = JsonConvert.DeserializeObject<List<AssignmentModelList>>(response);
                
                if(completeTaskList != null)
                {
                    return View(completeTaskList);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> AssignTaskList()
        {
            try
            {
                /*List<AssignmentModelList> AssignTaskList = taskRepository.AssignTaskList(SessionHelper.UserId);*/
                string url = $"api/TeacherAPI/AssignTaskList?teacherId={SessionHelper.UserId}";
                string res = await WebHelper.HttpRequestResponce(url);
                List<AssignmentModelList> AssignTaskList = JsonConvert.DeserializeObject<List<AssignmentModelList>>(res);
                return View(AssignTaskList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<ActionResult> PendingTaskList()
        {
            try
            {
                List<AssignmentModelList> assignments = new List<AssignmentModelList>();
                /*assignments = taskRepository.PendingTaskList(SessionHelper.UserId);*/
                string url = $"api/TeacherAPI/PendingTaskList?teacherId={SessionHelper.UserId}";
                string res = await WebHelper.HttpRequestResponce(url);
                assignments = JsonConvert.DeserializeObject<List<AssignmentModelList>>(res);
                if(assignments != null)
                {
                    return View(assignments);
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult PageNotFound()
        {
            return View("Error");
        }
    }
}