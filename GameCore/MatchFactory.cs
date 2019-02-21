using GameCore.Questions;
using GameCore.Questions.Templates;
using GameCore.Questions.Templates.Factory;
using GameCore.Questions.Visitor;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore
{
    public class MatchFactory : IMatchFactory
    {
        private readonly ITemplatesFactory _tempaltesFactory;
        private readonly IQuestionGenerator _questionGenerator;
        private readonly Random _random = new Random();
        private readonly int _maximumSameTypePerMatch;
        private readonly int _numberOfQuestions;
        private readonly IViewFinder _viewFinder;
        private Match _building;

        public MatchFactory(ITemplatesFactory templatesFactory,
            IConfiguration configuration,
            IQuestionGenerator questionGenerator,
            IViewFinder viewFinder)
        {
            _tempaltesFactory = templatesFactory;
            _questionGenerator = questionGenerator;
            _viewFinder = viewFinder;

            if (!int.TryParse(configuration.GetSection("GameSettings:MaxSameTypeQuizPerMatch").Value, out int result))
                result = 3;
            _maximumSameTypePerMatch = result;

            if (!int.TryParse(configuration.GetSection("GameSettings:NumberOfQuestions").Value, out result))
                result = 6;
            _numberOfQuestions = result;
        }

        public Match BuildMatch(string playerName)
        {
            _building = new Match(_viewFinder)
            {
                Player = new Domain.Entities.Player
                {
                    Name = playerName,
                    Score = 0
                }
            };

            _building.PendingQuestions.AddRange(GetQuestions());

            return _building;
        }

        public List<Question> GetQuestions()
        {
            var questions = new List<Question>();
            var questionsTemplates = _tempaltesFactory.GetAllTemplates();
            for (int i = 0; i < _numberOfQuestions; i++)
            {
                _building.PendingQuestions.Add(NextQuestion(questionsTemplates));
            }
            return questions;
        }

        public Question NextQuestion(List<QuestionTemplate> templates)
        {
            Question question;
            QuestionTemplate questionTemplate;

            int random;
            do {
                random = _random.Next(templates.Count);
                questionTemplate = templates[random];
            } while (questionTemplate.QuestionsGenerated == _maximumSameTypePerMatch);

            question = questionTemplate.Accept(_questionGenerator);

            return question;
        }
    }

    public interface IMatchFactory
    {
        Match BuildMatch(string playerName);
    }
}
