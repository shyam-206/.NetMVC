using MVCCrud.Sesssion;
using MVCCrud_Model.CustomModel;
using MVCCrud_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCCrud.Controllers
{
    public class LoginController : Controller
    {

        private readonly IRegister _repo;

        public LoginController(IRegister repo)
        {
            this._repo = repo;
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                List<RegisterModel> registerModelList = _repo.GetAllRegisterUserList();
                return View(registerModelList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                Session.Clear();
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Login(RegisterModel loginUserModel)
        {
            try
            {
                bool isUserExist = _repo.CheckingUserExist(loginUserModel);
                //is exist then redirect to dashboard theme
                if (isUserExist)
                {
                    SessionHelper.UserID = loginUserModel.UserID;
                    SessionHelper.UserName = loginUserModel.UserName;
                    ModelState.Clear();
                    TempData["SuccessLogin"] = "Login SuccessFully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorLogin"] = "Something Went to Wrong";
                    return Redirect("/");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RegisterModel register = _repo.AddRegister(registerModel);

                    if (register != null)
                    {
                        TempData["Register"] = "Register Successfully";
                        ModelState.Clear();
                        return RedirectToAction("/");
                    }
                    else
                    {
                        ModelState.AddModelError("UserEmail", "Email is already exist");
                    }

                }
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult Logout()
        {
            TempData["Logout"] = "Logout Successfully" ;
            Session.Clear();
            return Redirect("/");
        }
    }
}