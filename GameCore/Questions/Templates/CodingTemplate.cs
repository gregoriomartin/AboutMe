using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.Questions.Templates
{
    public class CodingTemplate : QuestionTemplate
    {
        public List<QuizAndAnswer<string, string>> QuizAndAnswers { get; private set; }

        public override Question Accept(IQuestionGenerator questionGenerator)
        {
            return questionGenerator.GenerateQuiz(this);
        }
        
        public CodingTemplate SetQuizAndAnswers(List<Quiz> quiz)
        {
            QuizAndAnswers = quiz.Where(q => q.Type == QuestionType.Common)
                .Select(q => new QuizAndAnswer<string, string> { CorrectAnswer = q.CorrectAnswer, Quiz = q.Text })
                .ToList();
            return this;
        }

        public override object GetNextQuiz()
        {
            int pos;

            do
            {
                pos = _random.Next(QuizAndAnswers.Count);
            } while (_positionsUsed.Any(pu => pu == pos));

            _positionsUsed.Add(pos);

            return QuizAndAnswers[pos];
        }
    }
}
