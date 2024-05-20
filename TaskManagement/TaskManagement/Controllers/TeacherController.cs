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
        public async Task<ActionResult> Index()
        {
            try
            {
                int teacherId = SessionHelper.UserId;
/*
                ViewBag.TotalTaskCount = taskRepository.GetTotaTaskCount(teacherId);
                ViewBag.AssignTaskCount = taskRepository.GetAllAssignTaskCount(teacherId);
                ViewBag.CompletedTaskCount = taskRepository.CompletedTaskCount(teacherId);
                ViewBag.PendingTaskCount = taskRepository.PendingTaskCount(teacherId);*/
                string TotalTaskURL = $"api/TeacherAPI/GetTotaTaskCount?teacherId={teacherId}";
                string AssignTaskURL = $"api/TeacherAPI/GetAllAssignTaskCount?teacherId={teacherId}";
                string CompletedTaskURL = $"api/TeacherAPI/CompletedTaskCount?teacherId={teacherId}";
                string PendingTaskURL = $"api/TeacherAPI/PendingTaskCount?teacherId={teacherId}";

                string resTotalTask = await WebHelper.HttpRequestResponse(TotalTaskURL);
                string resAssignTask = await WebHelper.HttpRequestResponse(AssignTaskURL);
                string resCompleteTask = await WebHelper.HttpRequestResponse(CompletedTaskURL);
                string resPendingTask = await WebHelper.HttpRequestResponse(PendingTaskURL);

                ViewBag.TotalTaskCount = JsonConvert.DeserializeObject<int>(resTotalTask);
                ViewBag.AssignTaskCount = JsonConvert.DeserializeObject<int>(resAssignTask);
                ViewBag.CompletedTaskCount = JsonConvert.DeserializeObject<int>(resCompleteTask);
                ViewBag.PendingTaskCount = JsonConvert.DeserializeObject<int>(resPendingTask);
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

        [HttpGet]
        public async Task<ActionResult> AssignTaskByTeacher()
        {
            try
            {
                /*List<TaskModel> taskModelList = taskRepository.GetAllTaskList(SessionHelper.UserId);*/
                List<TaskModel> taskModelList = new List<TaskModel>();
                int teacherId = SessionHelper.UserId;
                string url = $"api/TeacherAPI/GetAllTaskList?teacherId={teacherId}";
                string res = await WebHelper.HttpRequestResponse(url);
                taskModelList = JsonConvert.DeserializeObject<List<TaskModel>>(res);
                return View(taskModelList);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult> AssignTask(int TaskId)
        {
            try
            {
                ViewBag.TaskId = TaskId;
                /*ViewBag.AllStudentList = taskRepository.GetAllStudentList(TaskId);*/

                AssignmentModel assignmentModel = new AssignmentModel();
                string url = $"api/TeacherAPI/GetAllStudentList?Taskid={TaskId}";
                string res = await WebHelper.HttpRequestResponse(url);
                ViewBag.AllStudentList = JsonConvert.DeserializeObject<List<StudentModel>>(res);
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
                if (ModelState.IsValid)
                {

                    /*bool CheckSaveOrNot = taskRepository.AssignTask(assignmentModel);*/
                    string url1 = "api/TeacherAPI/AssignTask";
                    string jsonContent = JsonConvert.SerializeObject(assignmentModel);
                    string response = await WebHelper.HttpClientPostRequest(url1, jsonContent);
                    bool CheckSaveOrNot = JsonConvert.DeserializeObject<bool>(response);

                    if (CheckSaveOrNot)
                    {
                        TempData["Assgintask"] = "Task was assign to student";
                        return RedirectToAction("Index", "Teacher");
                    }
                }
                /*ViewBag.AllStudentList = taskRepository.GetAllStudentList(TaskId);*/
                string url = $"api/TeacherAPI/GetAllStudentList?Taskid={TaskId}";
                string res = await WebHelper.HttpRequestResponse(url);
                ViewBag.AllStudentList = JsonConvert.DeserializeObject<List<StudentModel>>(res);
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
                int count = 3;
                string response = await WebHelper.HttpRequestResponse(url);
                List<TaskModel> taskModel = JsonConvert.DeserializeObject<List<TaskModel>>(response);
                List<TaskModel> taskModelList = taskModel.Skip((currnetIndex - 1) * count).Take(count).ToList();

                PaginationModel paginationModel = new PaginationModel();
                paginationModel.taskList = taskModelList;
                paginationModel.CurrentIndex = currnetIndex;
                paginationModel.Count = taskModel.Count();

                return View(paginationModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> CompleteTaskList(int index = 1)
        {
            try
            {
                /*completeTaskList = taskRepository.CompleteTaskList(SessionHelper.UserId);*/
                List<AssignmentModelList> completeTaskList = new List<AssignmentModelList>();
                int teacherId = SessionHelper.UserId;
                string url = $"api/TeacherAPI/CompleteTaskList?teacherId={teacherId}";
                string response = await WebHelper.HttpRequestResponse(url);
                completeTaskList = JsonConvert.DeserializeObject<List<AssignmentModelList>>(response);

                int currentIndex = index;
                int count = 3;
                List<AssignmentModelList> list = completeTaskList.Skip((currentIndex - 1) * count).Take(count).ToList();
                PaginationModel paginationModel = new PaginationModel {
                    assignmentList = list,
                    CurrentIndex = currentIndex,
                    Count = completeTaskList.Count()
                };

                if (paginationModel != null)
                {
                    return View(paginationModel);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> AssignTaskList(int index = 1)
        {
            try
            {
                /*List<AssignmentModelList> AssignTaskList = taskRepository.AssignTaskList(SessionHelper.UserId);*/
                string url = $"api/TeacherAPI/AssignTaskList?teacherId={SessionHelper.UserId}";
                string res = await WebHelper.HttpRequestResponse(url);
                List<AssignmentModelList> AssignTaskList = JsonConvert.DeserializeObject<List<AssignmentModelList>>(res);

                int currentIndex = index;
                int count = 3;
                List<AssignmentModelList> list = AssignTaskList.Skip((currentIndex - 1) * count).Take(count).ToList();
                PaginationModel paginationModel = new PaginationModel
                {
                    assignmentList = list,
                    CurrentIndex = currentIndex,
                    Count = AssignTaskList.Count()
                };

                if(paginationModel != null)
                {
                    
                return View(paginationModel);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<ActionResult> PendingTaskList(int index = 1)
        {
            try
            {
                List<AssignmentModelList> assignments = new List<AssignmentModelList>();
                /*assignments = taskRepository.PendingTaskList(SessionHelper.UserId);*/
                string url = $"api/TeacherAPI/PendingTaskList?teacherId={SessionHelper.UserId}";
                string res = await WebHelper.HttpRequestResponse(url);
                assignments = JsonConvert.DeserializeObject<List<AssignmentModelList>>(res);

                int currentIndex = index;
                int count = 3;
                List<AssignmentModelList> list = assignments.Skip((currentIndex - 1) * count).Take(count).ToList();
                PaginationModel paginationModel = new PaginationModel { 
                    assignmentList = list ,
                    CurrentIndex = currentIndex,
                    Count = assignments.Count()
                };
                if (paginationModel != null)
                {
                    return View(paginationModel);
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