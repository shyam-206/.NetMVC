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
                }
                else
                {
                    ModelState.AddModelError("confirmPassword", "Password must be same");
                }
            }
            return View(registerModel);
        }
    }
}