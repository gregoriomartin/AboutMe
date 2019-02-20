using Domain.Entities;
using GameCore.GameModels;
using GameCore.Questions.Templates;
using GameCore.Questions.Visitor;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.Questions
{
    public class TitlesQuestion : Question
    {
        private readonly QuizAndAnswer<string, long> _quiz;

        public TitlesQuestion(string text, List<Title> titles)
        {
            Titles = titles;
            _quiz = new QuizAndAnswer<string, long> { Quiz = text, Answer = titles.Single(t => !t.Fake).Id };
        }

        public List<Title> Titles { get; set; }

        public override string QuestionText => _quiz.Quiz;

        public override bool IsCorrect()
        {
            long id = long.Parse(Answer);
            return _quiz.Answer == id;
        }

        public override string Accept(IViewFinder viewFinder)
        {
            return viewFinder.GetView(this);
        }

        public override BaseQuestionViewModel MakeViewModel()
        {
            return new ChooseOneViewModel()
            {
                Question = _quiz.Quiz,
                Titles = Titles
            };
        }
    }
}
