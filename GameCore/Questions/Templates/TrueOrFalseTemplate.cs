using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GameCore.Questions.Templates
{
    public class TrueOrFalseTemplate : QuestionTemplate
    {
        public static readonly IList<QuizAndAnswer<string, bool>> Questions = new ReadOnlyCollection<QuizAndAnswer<string, bool>>(new[]
        {
            new QuizAndAnswer<string, bool> { Quiz =  "Does C# supports anonymous inner classes?", Answer = false },
            new QuizAndAnswer<string, bool> { Quiz =  "If an App has bad performance, is it advisable to call the garbage collector to make it release memory more quickly?", Answer = false },
            new QuizAndAnswer<string, bool> { Quiz =  $"Are you ready to be the team leader of {QuestionTemplate.GameMasterName}?", Answer = true }
        });

        public override Question Accept(IQuestionGenerator questionGenerator)
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
