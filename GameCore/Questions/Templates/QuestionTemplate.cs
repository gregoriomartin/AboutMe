using System;
using System.Collections.Generic;

namespace GameCore.Questions.Templates
{
    public abstract class QuestionTemplate : IVisitable
    {
        protected readonly List<int> _positionsUsed = new List<int>();
        protected readonly Random _random = new Random();

        public static string GameMasterName { get; set; }

        public abstract Question Accept(IQuestionGenerator questionGenerator);
        public abstract object GetNextQuiz();

        public static T GetInstance<T>() where T : QuestionTemplate, new()
        {
            T instance = new T();
            return instance;
        }
    }
}
