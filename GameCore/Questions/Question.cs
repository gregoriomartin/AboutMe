using GameCore.GameModels;
using GameCore.Questions.Visitor;

namespace GameCore.Questions
{
    public abstract class Question
    {
        public string Answer { get; set; }
        public abstract string QuestionText { get; }
        public abstract bool IsCorrect();
        public abstract string Accept(IViewFinder viewFinder);
        public abstract BaseQuestionViewModel MakeViewModel();
    }
}
