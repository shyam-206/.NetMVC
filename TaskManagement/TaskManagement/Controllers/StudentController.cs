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
        public ActionResult Index()
        {
            try
            {
                int studentId = SessionHelper.UserId;

                ViewBag.TotalAssignmentCount = studentRepository.GetTaskAssignByTeacherCount(studentId);
                ViewBag.CompleteAssignMentCount = studentRepository.GetCompleteTaskCount(studentId);
                ViewBag.PendingAssignmentCount = studentRepository.GetPendingTaskCount(studentId);
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult Assignment()
        {
            try
            {

                int studentId = SessionHelper.UserId;
                List<Assignment> assignments = studentRepository.GetAllTaskAssignByTeacher(studentId);
                return View(assignments);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult SetAssignmentStatus(int id)
        {

            bool setStatus = studentRepository.AssignmentStatusUpdate(id);

            if (setStatus)
            {
                TempData["CompleteTask"] = "Task is Completed";
                return RedirectToAction("Assignment", "Student");
            }

            return RedirectToAction("Assignment");
        }

        public ActionResult Detail(int TaskID)
        {
            try
            {
                TaskModel taskModel = new TaskModel();


                taskModel = taskRepository.GetTaskByTaskId(TaskID);
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

        public ActionResult PageNotFound()
        {
            return View("Error");
        }


    }
}