using Domain.Entities;
using GameCore.Questions.Templates;

namespace GameCore.Questions
{
    public class QuestionGenerator : IQuestionGenerator
    {
        public Question GenerateQuiz(CodingTemplate type)
        {
            var quiz = (QuizAndAnswer<string, string>)type.GetNextQuiz();
            return new CommonQuestion(quiz);
        }

        public Question GenerateQuiz(ChooseOneTitleTemplate type)
        {
            return (Question)type.GetNextQuiz();
        }

        public Question GenerateQuiz(TrueOrFalseTemplate type)
        {
            var quiz = (QuizAndAnswer<string, bool>)type.GetNextQuiz();
            return new TrueOrFalseQuestion(quiz);
        }
    }
}
