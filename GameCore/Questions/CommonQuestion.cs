using GameCore.GameModels;
using GameCore.Questions.Templates;
using GameCore.Questions.Visitor;

namespace GameCore.Questions
{
    public class CommonQuestion : Question
    {
        private readonly QuizAndAnswer<string, string> _quiz;

        public CommonQuestion(QuizAndAnswer<string, string> quiz)
        {
            _quiz = quiz;
        }

        public override string QuestionText => _quiz.Quiz;

        public override string Accept(IViewFinder viewFinder)
        {
            return viewFinder.GetView(this);
        }

        public override bool IsCorrect()
        {
           return _quiz.CorrectAnswer.ToLower().Equals(Answer?.ToLower().Trim());
        }

        public override BaseQuestionViewModel MakeViewModel()
        {
            return new BaseQuestionViewModel()
            {
                Question = _quiz.Quiz
            };
        }
    }
}
