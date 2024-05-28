using QuizManagement.Session;
using QuizManagement_Model.ViewModel;
using QuizManagement_Repository.Interface;
using QuizManagement_Repository.Service;
using System;
using System.Web.Mvc;

namespace QuizManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository loginRepository;
        
        public LoginController()
        {
            loginRepository = new LoginService();
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                AdminModel checkAdmin = loginRepository.CheckAdminExist(loginModel);
                UserModel checkUser = loginRepository.CheckUserExist(loginModel);
                if (checkAdmin != null && checkAdmin.admin_id  > 0)
                {
                    SessionHelper.UserId = checkAdmin.admin_id;
                    SessionHelper.Username = checkAdmin.username;
                    SessionHelper.Useremail = checkAdmin.email;
                    SessionHelper.Role = "Admin";
                    TempData["Email"] = checkAdmin.email;
                    TempData["LoginAdmin"] = "Admin Login Successfully";
                    return RedirectToAction("Index", "Admin");
                }
                else if (checkUser != null && checkUser.user_id > 0)
                {
                    SessionHelper.UserId = checkUser.user_id;
                    SessionHelper.Username = checkUser.username;
                    SessionHelper.Useremail = checkUser.email;
                    SessionHelper.Role = "User";
                    TempData["Email"] = checkUser.email;
                    TempData["LoginUser"] = "User Login Successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Wrong"] = "Username or password is wrong";
                    return View(loginModel);
                }
            }
            return View(loginModel);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel registerModel)
        {
            if (ModelState.IsValid)
            {
                UserModel userModel = new UserModel();
                userModel = registerModel;

                if(userModel.password == userModel.confirmPassword)
                {
                    bool isAddUser = loginRepository.AddRegister(userModel);

                    if (isAddUser)
                    {
                        TempData["register"] = "Register Successfully";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("email", "Email is already exist");
                    }
                }
                else
                {
                    
                    ModelState.AddModelError("confirmPassword", "Password must be same");
                }
            }
            return View(registerModel);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            TempData["logout"] = "Logout Successfully";
            return RedirectToAction("Login");
        }

       
    }
}