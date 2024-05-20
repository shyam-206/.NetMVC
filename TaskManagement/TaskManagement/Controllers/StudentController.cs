using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagement.CustomFilter;
using TaskManagement.Session;
using Taskmanagement_Repository.Interface;
using Taskmanagement_Repository.Service;
using TaskManagement_Model.DBContext;
using TaskManagement_Model.ViewModel;
using TaskManagement.Common;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaskManagement.Controllers
{
    [CustomAuthorization]

    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly ITaskRepository taskRepository;

        public StudentController()
        {
            studentRepository = new StudentService();
            taskRepository = new TaskService();
        }
        // GET: Student
        public async Task<ActionResult> Index()
        {
            try
            {
                int studentId = SessionHelper.UserId;
                /*ViewBag.TotalAssignmentCount = studentRepository.GetTaskAssignByTeacherCount(studentId);
                ViewBag.CompleteAssignMentCount = studentRepository.GetCompleteTaskCount(studentId);
                ViewBag.PendingAssignmentCount = studentRepository.GetPendingTaskCount(studentId);*/

                string TotalAssignmentURL = $"api/StudentAPI/GetTaskAssignByTeacherCount?studentId={studentId}";
                string CompleteAssignMentURL = $"api/StudentAPI/GetCompleteTaskCount?studentId={studentId}";
                string PendingAssignmentURL= $"api/StudentAPI/GetPendingTaskCount?studentId={studentId}";
                string resTotalAssignment = await WebHelper.HttpRequestResponse(TotalAssignmentURL);
                string resCompleteAssignment = await WebHelper.HttpRequestResponse(CompleteAssignMentURL);
                string resPendingAssignment = await WebHelper.HttpRequestResponse(PendingAssignmentURL);

                ViewBag.TotalAssignmentCount = JsonConvert.DeserializeObject<int>(resTotalAssignment);
                ViewBag.CompleteAssignMentCount = JsonConvert.DeserializeObject<int>(resCompleteAssignment);
                ViewBag.PendingAssignmentCount = JsonConvert.DeserializeObject<int>(resPendingAssignment);

                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> Assignment(int index = 1)
        {
            try
            {
                int studentId = SessionHelper.UserId;
                /*List<AssignmentModelList> assignments = studentRepository.GetAllTaskAssignByTeacher(studentId);*/
                string url = $"api/StudentAPI/GetAllTaskAssignByTeacher?studentId={studentId}";
                string res = await WebHelper.HttpRequestResponse(url);
                List<AssignmentModelList> assignments = JsonConvert.DeserializeObject<List<AssignmentModelList>>(res);

                int currentIndex = index;
                int count = 3;
                List<AssignmentModelList> list = assignments.Skip((currentIndex - 1) * count).Take(count).ToList();
                PaginationModel paginationModel = new PaginationModel { 
                    assignmentList = list,
                    CurrentIndex = currentIndex,
                    Count = assignments.Count()
                };

                return View(paginationModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
        public async Task<ActionResult> SetAssignmentStatus(int id)
        {

            /*bool setStatus = studentRepository.AssignmentStatusUpdate(id);*/
            string url = $"api/StudentAPI/SetAssignmentStatus?id={id}";
            string content = JsonConvert.SerializeObject(id);
            string res = await WebHelper.HttpClientPostRequest(url, content);
            bool setStatus = JsonConvert.DeserializeObject<bool>(res);

            if (setStatus)
            {
                TempData["CompleteTask"] = "Task is Completed";
                return RedirectToAction("Assignment", "Student");
            }

            return RedirectToAction("Assignment");
        }

        public async Task<ActionResult> Detail(int TaskId)
        {
            try
            {
                TaskModel taskModel = new TaskModel();
                /*taskModel = taskRepository.GetTaskByTaskId(TaskID);*/
                string url = $"api/StudentAPI/GetTaskByTaskId?TaskId={TaskId}";
                string res = await WebHelper.HttpRequestResponse(url);
                taskModel = JsonConvert.DeserializeObject<TaskModel>(res);
                if (taskModel != null)
                {
                    return PartialView("Detail",taskModel);
                }

                return View("Assignment");
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<ActionResult> CompleteList(int index = 1)
        {
            try
            {
                int studentId = SessionHelper.UserId;
                /*List<AssignmentModelList> CompleteList = studentRepository.GetCompleteTaskList(studentId);*/
                string url = $"api/StudentAPI/CompleteList?studentId={studentId}";
                string res = await WebHelper.HttpRequestResponse(url);
                List<AssignmentModelList> CompleteList = JsonConvert.DeserializeObject<List<AssignmentModelList>>(res);
                int currentIndex = index;
                int count = 3;
                List<AssignmentModelList> list = CompleteList.Skip((currentIndex - 1) * count).Take(count).ToList();
                PaginationModel paginationModel = new PaginationModel {
                    assignmentList = list,
                    CurrentIndex = currentIndex,
                    Count = CompleteList.Count()
                };

                return View(paginationModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ActionResult> PendingList(int index = 1)
        {
            try
            {
                int studentId = SessionHelper.UserId;
                /*List<AssignmentModelList> PendingTaskList = studentRepository.GetPendingTaskList(studentId);*/
                List<AssignmentModelList> PendingTaskList = new List<AssignmentModelList>();
                string url = $"api/StudentAPI/PendingList?studentId={studentId}";
                string res = await WebHelper.HttpRequestResponse(url);
                PendingTaskList = JsonConvert.DeserializeObject<List<AssignmentModelList>>(res);

                int currentIndex = index;
                int count = 3;
                List<AssignmentModelList> list = PendingTaskList.Skip((currentIndex - 1) * count).Take(count).ToList();
                PaginationModel paginationModel = new PaginationModel { 
                    assignmentList = list,
                    CurrentIndex = currentIndex,
                    Count = PendingTaskList.Count()
                };
                return View(paginationModel);
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