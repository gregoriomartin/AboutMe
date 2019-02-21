using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore.Questions.Templates
{
    public class QuizAndAnswer<TQuiz, TAnswer>
    {
        public TQuiz Quiz { get; set; }
        public TAnswer CorrectAnswer { get; set; }
    }
}
