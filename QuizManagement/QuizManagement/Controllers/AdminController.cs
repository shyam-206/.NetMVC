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

        
    }
}