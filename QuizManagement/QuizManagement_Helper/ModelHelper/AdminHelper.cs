using QuizManagement_Model.DbContext;
using QuizManagement_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagement_Helper.ModelHelper
{
    public class AdminHelper
    {
        public static Quiz ConvertQuizToQuizModel(QuizModel quizModel)
        {
            Quiz quiz = new Quiz();
            quiz.quiz_id = quizModel.quiz_id;
            quiz.title = quizModel.title;
            quiz.description = quizModel.description;
            quiz.created_By = quizModel.created_By;
            quiz.created_at = DateTime.Now;
            return quiz;
        }

        public static List<QuizModel> ConvertQuizListToQuizModelList(List<Quiz> quizes,int userId)
        {
            try
            {
                List<QuizModel> quizModelList = new List<QuizModel>();
                if(quizes != null)
                {
                    foreach(Quiz quiz in quizes)
                    {
                        QuizModel quizModel = new QuizModel();
                        quizModel.quiz_id = quiz.quiz_id;
                        quizModel.title = quiz.title;
                        quizModel.description = quiz.description;
                        quizModel.created_By = quiz.created_By;
                        quizModel.is_completed = quiz.Result.FirstOrDefault(m => m.quiz_id == quiz.quiz_id && m.user_id == userId) != null ? 1 : 0;
                        quizModelList.Add(quizModel);
                    }
                }
                return quizModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<QuizModel> ConvertQuizListToQuizModelList(List<Quiz> quizes)
        {
            try
            {
                List<QuizModel> quizModelList = new List<QuizModel>();
                if (quizes != null)
                {
                    foreach (Quiz quiz in quizes)
                    {
                        QuizModel quizModel = new QuizModel();
                        quizModel.quiz_id = quiz.quiz_id;
                        quizModel.title = quiz.title;
                        quizModel.description = quiz.description;
                        quizModel.created_By = quiz.created_By;
                        quizModelList.Add(quizModel);
                    }
                }
                return quizModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static QuizModel ConvertQuizToQuizModel(Quiz quiz)
        {
            try
            {
                QuizModel quizModel = new QuizModel
                {
                    quiz_id = quiz.quiz_id,
                    title = quiz.title,
                    description = quiz.description,
                    created_By = quiz.created_By
                };

                quizModel.QuestionModelList = ConvertQuestionListToQuestionModelList(quiz.Question.ToList());
             
                return quizModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<QuestionModel> ConvertQuestionListToQuestionModelList(List<Question> questions)
        {
            try
            {
                List<QuestionModel> questionModelList = new List<QuestionModel>();
                if(questions != null)
                {
                    foreach(Question question in questions)
                    {
                        QuestionModel questionModel = new QuestionModel
                        {
                            ques_id = question.ques_id,
                            ques_text = question.ques_text,
                            quiz_id = question.quiz_id
                        };

                        questionModel.OptionList = ConvertOptionListToOptionModelList(question.Options.ToList());
                        questionModelList.Add(questionModel);
                    }
                }

                return questionModelList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<OptionModel> ConvertOptionListToOptionModelList(List<Options> options)
        {
            
            try
            {
                List<OptionModel> optionModelList = new List<OptionModel>();
                if(options != null)
                {
                    foreach(Options option in options)
                    {
                        OptionModel optionModel = new OptionModel
                        {
                            option_id = option.option_id,
                            option_text = option.option_text,
                            is_correct = (bool)option.is_correct,
                            ques_id = option.ques_id
                        };

                        optionModelList.Add(optionModel);
                    }
                }
                return optionModelList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
