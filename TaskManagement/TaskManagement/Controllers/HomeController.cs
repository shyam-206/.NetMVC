using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public HomeController()
        {
            this.cityRepository = new CityService();
            this.stateRepository = new StateService();
            this.studentRepository = new StudentService();
        }
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(RegisterModel _login)
        {
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.States = stateRepository.stateModelList();
            return View();
        }

        [HttpPost]
        public  ActionResult Register(RegisterModel registerModel)
        {
            return View();
        }
        

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CitiesByState(int id)
        {
            List<CityModel> list = cityRepository.GetCityByStateId(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}