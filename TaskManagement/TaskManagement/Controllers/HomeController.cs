using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagement.Session;
using TaskManagement_Model.DBContext;
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
        public ActionResult Login(LoginModel _loginUserModel)
        {
            try
            {

                if(ModelState.IsValid)
                {
                    if (_loginUserModel.Role == UserRole.Student)
                    {
                        bool isStudentUserExist = loginRepository.CheckingStudentExist(_loginUserModel);
                        Student student = loginRepository.CheckingStudent(_loginUserModel);
                        if (isStudentUserExist && student != null)
                        {
                            SessionHelper.UserId = student.StudentID;
                            SessionHelper.Username = _loginUserModel.Username;
                            SessionHelper.UserRole = "Student";
                            TempData["StudentLogin"] = "Student is login successfully";
                            return RedirectToAction("Index", "Student");
                        }
                    }

                    if (_loginUserModel.Role == UserRole.Teacher)
                    {

                        bool isTeacherUserExist = loginRepository.CheckingTeacherExist(_loginUserModel);
                        Teacher teacher = loginRepository.CheckingTeacher(_loginUserModel);
                        if (isTeacherUserExist)
                        {
                            SessionHelper.UserId = teacher.TeacherID;
                            SessionHelper.Username = teacher.Username;
                            SessionHelper.UserRole = "Teacher";
                            TempData["TeacherLogin"] = "Teacher is login successfully";
                            return RedirectToAction("Index", "Teacher");
                        }
                    }

                    return View(_loginUserModel);
                }
                else
                {
                    return View(_loginUserModel);
                }
                
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
                            TempData["StudentRegister"] = "Student Register Successfully";
                            return RedirectToAction("Login");
                        }
                    }
                    if (registerModel.Role == UserRole.Teacher)
                    {
                        bool isCheckingSaveOrNot = teacherRepository.AddTeacher(registerModel);
                        if (isCheckingSaveOrNot)
                        {
                            ModelState.Clear();
                            TempData["TeacherRegister"] = "Teacher Register Successfully";
                            return RedirectToAction("Login");
                        }
                     }

                    ModelState.AddModelError("Email", "Email is already exist");
                    ViewBag.States = stateRepository.stateModelList();
                    return View(registerModel);
                }
                else
                {
                    ViewBag.States = stateRepository.stateModelList();
                    return View(registerModel);
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
            TempData["Logout"] = "Logout Successfully";
            return Redirect("/");
        }
        public JsonResult CitiesByState(int id)
        {
            List<CityModel> list = cityRepository.GetCityByStateId(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}