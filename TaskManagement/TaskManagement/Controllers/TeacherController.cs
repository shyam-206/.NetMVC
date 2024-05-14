using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagement.CustomFilter;
using TaskManagement.Session;
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
        public ActionResult Addtask(TaskModel taskModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isCheckingSaveOrNot = taskRepository.AddTask(taskModel);
                    if (isCheckingSaveOrNot)
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
                
                ViewBag.AllStudentList = taskRepository.GetAllStudentList(TaskId);
                return PartialView("AssignTask");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult Tasklist()
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

        public ActionResult PageNotFound()
        {
            return View("Error");
        }
    }
}