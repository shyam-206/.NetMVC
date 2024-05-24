using QuizManagement.Session;
using QuizManagement_Model.ViewModel;
using QuizManagement_Repository.Interface;
using QuizManagement_Repository.Service;
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
                    TempData["Email"] = checkAdmin.email;
                    return RedirectToAction("Index", "Admin");
                }
                else if (checkUser != null && checkUser.user_id > 0)
                {
                    SessionHelper.UserId = checkUser.user_id;
                    SessionHelper.Username = checkUser.username;
                    SessionHelper.Useremail = checkUser.email;
                    TempData["Email"] = checkUser.email;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
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
            return RedirectToAction("Login");
        }
    }
}