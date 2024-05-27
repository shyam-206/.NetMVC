using QuizManagement.Session;
using QuizManagement_Model.ViewModel;
using QuizManagement_Repository.Interface;
using QuizManagement_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRespository repository;

        public AdminController()
        {
            repository = new AdminService();
        }
        public ActionResult Index()
        {
            ViewBag.adminId = SessionHelper.UserId;
            List<QuizModel> quizModelList = repository.GetAllQuizModelList();
            return View(quizModelList);
        }

        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(QuizModel quizModel)
        {
            if (ModelState.IsValid)
            {
                bool addQuiz = repository.AddQuiz(quizModel);

                if (addQuiz)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(quizModel);
        }

        public ActionResult AdminProfile(int adminId)
        {
            try
            {
                AdminModel adminModel = new AdminModel();
                adminModel = repository.GetAdminProfile(adminId);
                if(adminModel != null  && adminModel.admin_id > 0)
                {
                    return View(adminModel);
                }

                return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AdminProfile(AdminModel adminModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool CheckUpdateProfile = repository.UpdateAdminProfile(adminModel);

                    if (CheckUpdateProfile)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        
                        return View(adminModel);
                    }
                }
                else
                {
                    return View(adminModel);
                }
           
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult EditQuiz(int quiz_id)
        {
            QuizModel quizModel = repository.GetQuizById(quiz_id);
            return View(quizModel);
        }

        [HttpPost]
        public ActionResult EditQuiz(QuizModel quizModel)
        {
            try
            {
                bool CheckUpdateOrNot = repository.UpdateQuizById(quizModel);

                if (CheckUpdateOrNot)
                {
                    return RedirectToAction("Index");
                }

                return View(quizModel);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}