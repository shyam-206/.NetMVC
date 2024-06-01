using CourseManagement.Session;
using CourseManagement_Model.ViewModel;
using CourseManagement_Repository.Interface;
using CourseManagement_Repository.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseManagement.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly InstructorRepository instructorRepository;
        public InstructorController()
        {
            courseRepository = new CourseService();
            instructorRepository = new InstructorService();
        }
        public ActionResult Index()
        {
            int Id = SessionHelper.UserId;
            List<CourseModel> courseModelList = courseRepository.GetCourseList(Id);
            return View(courseModelList);
        }
        public ActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCourse(CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {
                bool CheckSave = courseRepository.AddCourse(courseModel);
                return RedirectToAction("Index");
            }
            return View(courseModel);
        }

        public ActionResult UploadMaterial(int CourseId)
        {
            ViewBag.CourseId = CourseId;
            return View();
        }
        [HttpPost]
        public ActionResult UploadMaterial(MaterialModel materialModel)
        {
            string a = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                a += ConvertFileToString(Request.Files[i]);
                if (Request.Files.Count - 1 > i)
                    a += ",";
            }
            materialModel.FilePath = a;

            bool CheckSaveMaterial = instructorRepository.UploadMaterial(materialModel);
            if (CheckSaveMaterial)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(materialModel);
            }
        }
        public string ConvertFileToString(HttpPostedFileBase file)
        {
            string uniqefilename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            file.SaveAs(HttpContext.Server.MapPath("~/Content/UploadFiles/") + uniqefilename);
            return uniqefilename;
        }
    }
}