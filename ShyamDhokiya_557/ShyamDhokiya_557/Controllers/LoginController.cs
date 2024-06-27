using Newtonsoft.Json;
using ShyamDhokiya_557.Common;
using ShyamDhokiya_557.Session;
using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShyamDhokiya_557.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(loginModel.Role == "Admin")
                    {
                        string url = "api/LoginAPI/IsAdminExist";
                        string content = JsonConvert.SerializeObject(loginModel);
                        string res = await WebHelper.HttpRequestResponsePost(url, content);

                        UserModel userModel = JsonConvert.DeserializeObject<UserModel>(res);
                        if (userModel != null && userModel.UserId > 0)
                        {
                            SessionHelper.Role = "Admin";
                            SessionHelper.UserEmail = userModel.Email;
                            SessionHelper.UserId = userModel.UserId;
                            var cookie = new HttpCookie("jwt", userModel.Token)
                            {
                                HttpOnly = true,
                                 Secure = true,
                            };
                            Response.Cookies.Add(cookie);
                            TempData["Admin"] = "Login Succesully";
                            return RedirectToAction("Product", "Admin");
                        }
                        else
                        {
                            TempData["NotExist"] = "Not Exist";
                            return View(loginModel);
                        }
                        

                    }
                    else if(loginModel.Role == "Suppier")
                    {
                        string url = "api/LoginAPI/IsUserExist";
                        string content = JsonConvert.SerializeObject(loginModel);
                        string res = await WebHelper.HttpRequestResponsePost(url, content);

                        UserModel userModel = JsonConvert.DeserializeObject<UserModel>(res);
                        

                        if (userModel != null && userModel.UserId > 0)
                        {
                            SessionHelper.Role = "Suppier";
                            SessionHelper.UserEmail = userModel.Email;
                            SessionHelper.UserId = userModel.UserId;
                            var cookie = new HttpCookie("jwt", userModel.Token)
                            {
                                HttpOnly = true,
                                Secure = true,
                            };
                            Response.Cookies.Add(cookie);
                            TempData["LoginSuppier"] = "Login Succesully";
                            return RedirectToAction("Index", "Suppier");
                        }
                        else
                        {
                            TempData["NotExist"] = "Not Exist";
                            return View(loginModel);
                        }
                       
                    }
                    else
                    {
                        TempData["NotExist"] = "Not Exist";
                        return View(loginModel);
                    }
                }
                else
                {
                    TempData["LoginFields"] = "Please Enter Fields";
                    return View(loginModel);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    if(userModel.Password == userModel.ConfirmPassword)
                    {
                        string url = "api/LoginAPI/AddRegister";
                        string content = JsonConvert.SerializeObject(userModel);
                        string res = await WebHelper.HttpRequestResponsePost(url, content);
                        bool regiser = JsonConvert.DeserializeObject<bool>(res);

                        if (regiser)
                        {
                            TempData["RegisterDone"] = "Register Successfully";
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            TempData["UserExist"] = "UserExist";
                            return View(userModel);
                        }
                    }
                    else
                    {
                        TempData["password"] = "valid password";
                        return View(userModel);
                    }
                }
                else
                {
                    TempData["RegisterData"] = "Please Enter Fields";
                    return View(userModel);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult Logout()
        {
            TempData["Logout"] = "Logout Successfully";
            Session.Clear();
            return RedirectToAction("Login");
        }
    }


}