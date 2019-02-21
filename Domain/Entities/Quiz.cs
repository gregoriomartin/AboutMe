using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Quiz : IHasId
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string CorrectAnswer { get; set; }
        public QuestionType Type { get; set; }
    }

    public enum QuestionType
    {
        Common,
        TrueOrFalse
    }
}
