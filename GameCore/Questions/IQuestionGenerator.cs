using GameCore.Questions.Templates;

namespace GameCore.Questions
{
    public interface IQuestionGenerator
    {
        Question GenerateQuiz(CodingTemplate type);
        Question GenerateQuiz(ChooseOneTitleTemplate type);
        Question GenerateQuiz(TrueOrFalseTemplate type);
    }
}
