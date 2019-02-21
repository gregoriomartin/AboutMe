using System;
using System.Collections.Generic;
using System.Text;
using GameCore.GameModels;
using GameCore.Questions.Templates;
using GameCore.Questions.Visitor;

namespace GameCore.Questions
{
    public class TrueOrFalseQuestion : Question
    {
        private readonly QuizAndAnswer<string, bool> _quiz;

        public TrueOrFalseQuestion(QuizAndAnswer<string, bool> quiz)
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
            return bool.Parse(Answer) == _quiz.CorrectAnswer;
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

