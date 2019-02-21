using Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameCore.Questions.Templates
{
    public class TrueOrFalseTemplate : QuestionTemplate
    {
        public List<QuizAndAnswer<string, bool>> Questions { get; private set; }

        public TrueOrFalseTemplate SetQuestions(List<Quiz> questions)
        {
            Questions = questions.Where(q => q.Type == QuestionType.TrueOrFalse)
                .Select(q => new QuizAndAnswer<string, bool> { CorrectAnswer = bool.Parse(q.CorrectAnswer), Quiz = q.Text.Replace("{{GameMasterName}}", GameMasterName) })
                .ToList();
            return this;
        }

        protected override Question Handle(IQuestionGenerator questionGenerator)
        {
            return questionGenerator.GenerateQuiz(this);
        }

        public override object GetNextQuiz()
        {
            int pos;

            do
            {
                pos = _random.Next(Questions.Count);
            } while (_positionsUsed.Any(pu => pu == pos));

            _positionsUsed.Add(pos);

            return Questions[pos];
        }
    }
    
}
