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

namespace TaskManagement.Controllers
{
    [CustomAuthorization]
    
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;

        public StudentController()
        {
            studentRepository = new StudentService();
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

        public ActionResult PageNotFound()
        {
            return View("Error");
        }
    }
}