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
    public class LoginController : Controller
    {
        private readonly ILoginRepository loginRepository;

        public LoginController()
        {
            loginRepository = new LoginService();
        }
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            
            if (ModelState.IsValid)
            {
                UserModel userModel = loginRepository.CheckUserExist(loginModel);
                if (userModel != null && userModel.UserId > 0)
                {
                    if(userModel.Role == "Admin")
                    {
                        SessionHelper.UserId = userModel.UserId;
                        SessionHelper.Username = userModel.Username;
                        SessionHelper.Useremail = userModel.Email;
                        SessionHelper.Role = userModel.Role;
                        return RedirectToAction("Index", "Admin");
                    } 
                    if(userModel.Role == "Instructor")
                    {
                        SessionHelper.UserId = userModel.UserId;
                        SessionHelper.Username = userModel.Username;
                        SessionHelper.Useremail = userModel.Email;
                        SessionHelper.Role = userModel.Role;
                        return RedirectToAction("Index", "Instructor");
                    }
                    if(userModel.Role == "Student")
                    {
                        SessionHelper.UserId = userModel.UserId;
                        SessionHelper.Username = userModel.Username;
                        SessionHelper.Useremail = userModel.Email;
                        SessionHelper.Role = userModel.Role;
                        return RedirectToAction("CourseList","Student");
                    }
                    return View(loginModel);
                }
                else
                {
                    return View(loginModel);
                }
            }
            else
            {
                return View(loginModel);
            }
            
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel userModel)
        {
            
            if (ModelState.IsValid)
            {
                if (userModel.Password == userModel.ConfirmPassword)
                {
                    bool check = loginRepository.AddUser(userModel);
                    return RedirectToAction("Login");

                }
                else
                {
                    return View(userModel);
                }
            }
            else
            {
                return View(userModel);

            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}