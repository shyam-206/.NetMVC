using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagement.Session;
using TaskManagement_Model.ViewModel;
using Taskmanagement_Repository.Interface;
using Taskmanagement_Repository.Service;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICityRepository cityRepository;
        private readonly IStateRepository stateRepository;
        private readonly IStudentRepository studentRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly ILoginRepository loginRepository;

        public HomeController()
        {
            this.cityRepository = new CityService();
            this.stateRepository = new StateService();
            this.studentRepository = new StudentService();
            this.teacherRepository = new TeacherService();
            this.loginRepository = new LoginService();
        }
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Login(RegisterModel _loginUserModel)
        {
            try
            {
                if (_loginUserModel.Role == UserRole.Student)
                {
                    bool isStudentUserExist = loginRepository.CheckingStudentExist(_loginUserModel);

                    if (isStudentUserExist)
                    {
                        SessionHelper.UserId = 1;
                        SessionHelper.Username = _loginUserModel.Username;
                        SessionHelper.UserRole = "Student";
                        return RedirectToAction("Index", "Student");
                    }

                    return Redirect("/");
                }

                if (_loginUserModel.Role == UserRole.Teacher)
                {

                    bool isTeacherUserExist = loginRepository.CheckingTeacherExist(_loginUserModel);

                    if (isTeacherUserExist)
                    {
                        SessionHelper.UserId = 1;
                        SessionHelper.Username = _loginUserModel.Username;
                        SessionHelper.UserRole = "Teacher";
                        return RedirectToAction("Index","Teacher");
                    }

                    return Redirect("/");
                }

                return Redirect("/");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult Register()
        {
            ViewBag.States = stateRepository.stateModelList();
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (registerModel.Role == UserRole.Student)
                    {
                        bool isCheckingSaveOrNot = studentRepository.AddStudent(registerModel);
                        if (isCheckingSaveOrNot)
                        {
                            ModelState.Clear();
                            return View("Login");
                        }
                    }
                    if (registerModel.Role == UserRole.Teacher)
                    {
                        bool isCheckingSaveOrNot = teacherRepository.AddTeacher(registerModel);
                        if (isCheckingSaveOrNot)
                        {
                            ModelState.Clear();
                            return View("Login");
                        }
                    }
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("/");
        }
        public JsonResult CitiesByState(int id)
        {
            List<CityModel> list = cityRepository.GetCityByStateId(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}