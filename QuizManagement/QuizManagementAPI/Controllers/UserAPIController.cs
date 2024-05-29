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
    public class UserAPIController : ApiController
    {
        private readonly IAdminRespository repository;
        public UserAPIController()
        {
            repository = new AdminService();
        }

        [HttpGet]
        [Route("api/UserAPI/GetAllQuizModelList")]
        public List<QuizModel> GetAllQuizModelList(int userId)
        {
            try
            {
                List<QuizModel> quizModelList = repository.GetAllQuizModelList(userId);
                return quizModelList != null ? quizModelList : null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("api/UserAPI/GetQuizById")]

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
        [Route("api/UserAPI/AddAnswer")]
        public bool AddAnswer(List<AnswerModel> answerModelList)
        {
            try
            {
                bool CheckSaveAnswerList = repository.AddAnswer(answerModelList);
                return CheckSaveAnswerList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("api/UserAPI/GetUserProfile")]
        public UserModel GetUserProfile(int userId)
        {
            try
            {
                UserModel userModel = repository.GetUserProfile(userId);
                return userModel != null ? userModel : null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("api/UserAPI/UpdateUserProfile")]
        public bool UpdateUserProfile(UserModel userModel)
        {
            try
            {
                bool CheckUpdateProfile = repository.UpdateUserProfile(userModel);
                return CheckUpdateProfile;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("api/UserAPI/QuizScore")]
        public int QuizScore(int quiz_id,int user_id)
        {
            try
            {
                int score = repository.QuizScore(quiz_id, user_id);
                return score;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}