using QuizManagement_Model.ViewModel;
using QuizManagement_Repository.Interface;
using QuizManagement_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizManagementAPI.Controllers
{
    public class AdminAPIController : ApiController
    {
        private readonly IAdminRespository repository;
        public AdminAPIController()
        {
            repository = new AdminService();
        }

        [HttpGet]
        [Route("api/AdminAPI/AllQuizList")]
        public List<QuizModel> AllQuizList()
        {
            try
            {
                List<QuizModel> quizModelList = repository.AllQuizList();
                return quizModelList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("api/AdminAPI/AdminAPI")]
        public bool AdminAPI(QuizModel quizModel)
        {
            try
            {

                bool addquiz = repository.AddQuiz(quizModel);
                return addquiz;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("api/AdminAPI/GetAdminProfile")]
        public AdminModel GetAdminProfile(int adminId)
        {
            try
            {
                AdminModel adminModel = repository.GetAdminProfile(adminId);
                return adminModel != null ? adminModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("api/AdminAPI/UpdateAdminProfile")]
        public bool UpdateAdminProfile(AdminModel adminModel)
        {
            try
            {
                bool CheckUpdateProfile = repository.UpdateAdminProfile(adminModel);
                return CheckUpdateProfile;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("api/AdminAPI/GetQuizById")]
        public QuizModel GetQuizById(int quiz_id)
        {
            try
            {
                QuizModel quizModel = repository.GetQuizById(quiz_id);
                return quizModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("api/AdminAPI/UpdateQuizById")]
        public bool UpdateQuizById(QuizModel quizModel)
        {
            try
            {
                bool CheckUpdateOrNot = repository.UpdateQuizById(quizModel);
                return CheckUpdateOrNot;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("api/AdminAPI/DeleteQuiz")]
        public bool DeleteQuiz(int quiz_id)
        {
            try
            {
                bool deleteQuiz = repository.DeleteQuiz(quiz_id);
                return deleteQuiz;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}