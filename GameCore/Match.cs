using Domain.Entities;
using GameCore.Questions;
using GameCore.Questions.Visitor;
using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class Match
    {
        public Player Player { get; set; }
        public List<Question> PendingQuestions { get; set; }
        public List<Question> QuestionsAnswered { get; set; }
        private readonly IViewFinder _viewFinder;

        public Match(IViewFinder viewFinder)
        {
            _viewFinder = viewFinder;
            QuestionsAnswered = new List<Question>();
            PendingQuestions = new List<Question>();
        }
        

        public Stage NextStage(string lastAnswer)
        {
            var lastQuestionAnswered = PendingQuestions.First();
            lastQuestionAnswered.Answer = lastAnswer;
            QuestionsAnswered.Add(lastQuestionAnswered);
            PendingQuestions.Remove(lastQuestionAnswered);

            return NextStage();
        }

        public Stage NextStage()
        {
            var nextQuiz = PendingQuestions.First();

            string nextViewName = nextQuiz.Accept(_viewFinder);
            return new Stage() { ViewModel = nextQuiz.MakeViewModel(), ViewName = nextViewName };
        }
    }
}
