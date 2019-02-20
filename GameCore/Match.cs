using Domain.Entities;
using GameCore.GameModels;
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
        private Question _currentQuestion { get { return PendingQuestions.FirstOrDefault(); } }
        private bool _closed = false;

        public Match(IViewFinder viewFinder)
        {
            _viewFinder = viewFinder;
            QuestionsAnswered = new List<Question>();
            PendingQuestions = new List<Question>();
        }

        private int Score
        {
            get
            {
                int totalQuiz = QuestionsAnswered.Count;
                int score = (QuestionsAnswered.Count(qa => qa.IsCorrect()) * 100) / totalQuiz;
                return score;
            }
        }

        public void Answer(string answer)
        {
            var lastQuestionAnswered = PendingQuestions.First();
            lastQuestionAnswered.Answer = answer;
            QuestionsAnswered.Add(lastQuestionAnswered);
            PendingQuestions.Remove(lastQuestionAnswered);
            if (PendingQuestions.Count == 0)
                CloseMatch();
        }

        private void CloseMatch()
        {
            _closed = true;
            Player.Score = Score;
        }

        public Stage NextStage()
        {
            if (_closed)
                return new Stage()
                {
                    ViewName = "Score",
                    ViewModel = new ScoreViewModel()
                    {
                        Score = Score,
                        Question = "Leave me a message!"
                    }
                };

            string nextViewName = _currentQuestion.Accept(_viewFinder);
            return new Stage() { ViewModel = _currentQuestion.MakeViewModel(), ViewName = nextViewName };
        }
    }
}
