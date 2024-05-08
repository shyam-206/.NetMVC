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
            return View();
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
                bool isCheckingSaveOrNot = taskRepository.AddTask(taskModel);
                if (isCheckingSaveOrNot)
                {
                    return RedirectToAction("Index","Teacher");
                }

                return View(taskModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            /*return View(taskModel);*/
        }

        public ActionResult Assigntask()
        {
            ViewBag.AllStudentList = taskRepository.GetAllStudentList();
            ViewBag.TaskList = taskRepository.GetAllTaskList();

            return View();
        }

        [HttpPost]
        public ActionResult Assigntask(AssignmentModel assignmentModel)
        {
            try
            {
                bool isCheckSave = taskRepository.AssignTask(assignmentModel);
                if (isCheckSave)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}