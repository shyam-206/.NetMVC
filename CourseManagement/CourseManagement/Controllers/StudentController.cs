using CourseManagement.Session;
using CourseManagement_Model.ViewModel;
using CourseManagement_Repository.Interface;
using CourseManagement_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;

        public StudentController()
        {
            studentRepository = new StudentService();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CourseList()
        {
            int UserId = SessionHelper.UserId;
            List<CourseModel> list = studentRepository.GetAllCourseList(UserId);
            return View(list);
        }

        public ActionResult EnrollCourse(int CourseId)
        {
            try
            {
                int UserId = SessionHelper.UserId;
                bool EnrollCourse = studentRepository.EnrollmentCourse(CourseId, UserId);
                if (EnrollCourse)
                {
                    
                    return RedirectToAction("CourseList");
                }
                return RedirectToAction("ErrorPage");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public ActionResult PageNotFound()
        {
            return View("ErrorPage");
        }

        public ActionResult ViewMaterial(int CourseId)
        {
            MaterialModel materialModel = studentRepository.GetMaterial(CourseId);
            
            if(materialModel != null && materialModel.MaterialId > 0)
            {
                materialModel.Files = materialModel.FilePath.Split(',');
                return View(materialModel);
            }

            return RedirectToAction("CourseList");
            
        }
        
    }
}