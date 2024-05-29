using QuizManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Repository.Interface
{
    public interface IAdminRespository
    {
        bool AddQuiz(QuizModel quizModel);
        List<QuizModel> GetAllQuizModelList(int userId);
        List<QuizModel> AllQuizList();
        AdminModel GetAdminProfile(int adminId);
        bool UpdateAdminProfile(AdminModel adminModel);
        QuizModel GetQuizById(int quiz_id);
        bool UpdateQuizById(QuizModel quizModel);
        bool AddAnswer(List<AnswerModel> answerModelList);
        int QuizScore(int quiz_id, int user_id);
        bool DeleteQuiz(int quiz_id);
        UserModel GetUserProfile(int userId);
        bool UpdateUserProfile(UserModel userModel);
        List<ResultAnswerModel> AllAnswerList(int quiz_id,int user_id);
    }
}
