using Newtonsoft.Json;
using QuizManagement.Common;
using QuizManagement.Session;
using QuizManagement_Model.ViewModel;
using QuizManagement_Repository.Interface;
using QuizManagement_Repository.Service;
using System;
using System.Threading.Tasks;
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
            Session.Clear();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                /*AdminModel checkAdmin = loginRepository.CheckAdminExist(loginModel);
                UserModel checkUser = loginRepository.CheckUserExist(loginModel);*/
                string url = "api/LoginAPI/CheckAdminExist";
                string content = JsonConvert.SerializeObject(loginModel);
                string res = await WebHelper.HttpClientPostRequest(url, content);
                AdminModel checkAdmin = JsonConvert.DeserializeObject<AdminModel>(res);

                string res1 = await WebHelper.HttpClientPostRequest("api/LoginAPI/CheckUserExist",content);
                UserModel checkUser = JsonConvert.DeserializeObject<UserModel>(res1);
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
        public async Task<ActionResult> Register(UserModel registerModel)
        {
            if (ModelState.IsValid)
            {
                UserModel userModel = registerModel;
                if(userModel.password == userModel.confirmPassword)
                {
                    /*bool isAddUser = loginRepository.AddRegister(userModel);*/

                    string res = await WebHelper.HttpClientPostRequest("api/LoginAPI/AddRegister", JsonConvert.SerializeObject(userModel));
                    bool isAddUser = JsonConvert.DeserializeObject<bool>(res);
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