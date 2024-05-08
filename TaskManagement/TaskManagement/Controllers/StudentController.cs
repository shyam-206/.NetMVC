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
            return View();
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

    }
}