using System;
using System.Collections.Generic;

namespace GameCore.Questions.Templates
{
    public abstract class QuestionTemplate : IVisitable
    {
        public static string GameMasterName { get; set; }

        protected readonly List<int> _positionsUsed = new List<int>();
        protected readonly Random _random = new Random();

        public int QuestionsGenerated { get; private set; } = 0;

        public Question Accept(IQuestionGenerator questionGenerator)
        {
            QuestionsGenerated++;
            return Handle(questionGenerator);
        }

        protected abstract Question Handle(IQuestionGenerator questionGenerator);

        public abstract object GetNextQuiz();

        public static T GetInstance<T>() where T : QuestionTemplate, new()
        {
            T instance = new T();
            return instance;
        }
    }
}
