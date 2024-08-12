using Newtonsoft.Json;
using ShyamDhokiya_557.Common;
using ShyamDhokiya_557_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;
using ShyamDhokiya_557.Session;

namespace ShyamDhokiya_557.Controllers
{
    public class LoginController : Controller
    {
        private static readonly Random Random = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                if (loginModel.Role == "Admin")
                {
                    string url = "api/Login/IsAdminExist";
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
                        TempData["success"] = "Login Succesully";
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        TempData["error"] = "Not Exist";
                        return View(loginModel);
                    }


                }
                else if (loginModel.Role == "User")
                {
                    string url = "api/Login/IsUserExist";
                    string content = JsonConvert.SerializeObject(loginModel);
                    string res = await WebHelper.HttpRequestResponsePost(url, content);

                    UserModel userModel = JsonConvert.DeserializeObject<UserModel>(res);


                    if (userModel != null && userModel.UserId > 0)
                    {
                        SessionHelper.Role = "User";
                        SessionHelper.UserEmail = userModel.Email;
                        SessionHelper.UserId = userModel.UserId;
                        Session["ProfileImage"] = userModel.Image;
                        var cookie = new HttpCookie("jwt", userModel.Token)
                        {
                            HttpOnly = true,
                            Secure = true,
                        };
                        Response.Cookies.Add(cookie);
                        TempData["success"] = "Login Succesully";
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        TempData["error"] = "Not Exist";
                        return View(loginModel);
                    }

                }
                else
                {
                    TempData["error"] = "Not Exist";
                    return View(loginModel);
                }
            }
            else
            {
                TempData["error"] = "Please Enter Fields";
                return View(loginModel);
            }
        }

        public async Task<ActionResult> Register()
        {
            string res = await WebHelper.HttpRequestResponse("api/Login/GetCountryList");
            ViewBag.CountryList = JsonConvert.DeserializeObject<List<CountryModel>>(res);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {

                if (Request.Files.Count > 0  || userModel.Image.EndsWith("JPG") || userModel.Image.EndsWith("PNG") || userModel.Image.EndsWith("JPEG"))
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if(file != null && file.ContentLength < 3 * 1024 * 1024)
                    {
                        TempData["error"] = "Please Image Should be less than 3 MB";
                        return View(userModel);
                    }
                    userModel.Password = GenerateRandomPassword();
                    userModel.Image = GetUniqueImage(Request.Files[0]);
                    EmailModel emailModel = new EmailModel()
                    {
                        To = userModel.Email,
                        Subject = "Your Login Credentials",
                        Body = $"Login Password = {userModel.Password}"
                    };
                    SentEmail(emailModel);
                    string res = await WebHelper.HttpRequestResponsePost("api/Login/AddRegsiter", JsonConvert.SerializeObject(userModel));
                    bool add = JsonConvert.DeserializeObject<bool>(res);

                    if (add)
                    {
                        TempData["success"] = "User Register successfully";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["error"] = "Email Id Is already exist";
                        return View(userModel);
                    }
                }
                else
                {
                    TempData["error"] = "please valid a Image";
                    return View(userModel);
                }

            }
            else
            {
                TempData["error"] = "Please Select Required Fields";
                return View(userModel);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            TempData["success"] = "Logout Successfully";
            return RedirectToAction("Login");
        }
        private string GetUniqueImage(HttpPostedFileBase file)
        {
            string fileName = DateTime.Now.ToString("dd-MM-yyyy-ss") + file.FileName;
            file.SaveAs(HttpContext.Server.MapPath("~/Content/Proflies/") + fileName);
            return fileName;
        }

        public async Task<JsonResult> GetStateById(int CountryId)
        {
            string res = await WebHelper.HttpRequestResponse($"api/Login/GetStateById?CountryId={CountryId}");
            List<StateModel> list = JsonConvert.DeserializeObject<List<StateModel>>(res);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCityById(int @StateId)
        {
            string res = await WebHelper.HttpRequestResponse($"api/Login/GetCityById?StateId={StateId}");
            List<CityModel> list = JsonConvert.DeserializeObject<List<CityModel>>(res);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private string GenerateRandomPassword(int length = 6)
        {
            return new string(Enumerable.Repeat(Chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        private void SentEmail(EmailModel model)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            using (MailMessage mm = new MailMessage(smtpSection.From, model.To))
            {
                mm.Subject = model.Subject;
                mm.Body = model.Body;
                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = smtpSection.Network.Host;
                    smtp.EnableSsl = smtpSection.Network.EnableSsl;
                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    smtp.Credentials = networkCred;
                    smtp.Port = smtpSection.Network.Port;
                    smtp.Send(mm);
                }
            }

        }
    }
}