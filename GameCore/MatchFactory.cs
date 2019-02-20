using GameCore.Questions;
using GameCore.Questions.Templates.Factory;
using GameCore.Questions.Visitor;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace GameCore
{
    public class MatchFactory : IMatchFactory
    {
        private readonly ITemplatesFactory _tempaltesFactory;
        private readonly IQuestionGenerator _questionGenerator;
        private readonly Random _random = new Random();
        private readonly Dictionary<Type, int> _typeQuant;
        private readonly int _maximumSameTypePerMatch;
        private readonly int _numberOfQuestions;
        private readonly IViewFinder _viewFinder;

        public MatchFactory(ITemplatesFactory templatesFactory,
            IConfiguration configuration,
            IQuestionGenerator questionGenerator,
            IViewFinder viewFinder)
        {
            _tempaltesFactory = templatesFactory;
            _typeQuant = new Dictionary<Type, int>();
            _questionGenerator = questionGenerator;
            _viewFinder = viewFinder;

            if (!int.TryParse(configuration.GetSection("GameSettings:MaxSameTypeQuizPerMatch").Value, out int result))
                result = 3;
            _maximumSameTypePerMatch = result;

            if (!int.TryParse(configuration.GetSection("GameSettings:NumberOfQuestions").Value, out result))
                result = 6;
            _numberOfQuestions = 6;
        }

        public Match BuildMatch(string playerName)
        {
            Match match = new Match(_viewFinder)
            {
                Player = new Domain.Entities.Player
                {
                    Name = playerName,
                    Points = 0
                }
            };

            for (int i = 0; i < _numberOfQuestions; i++) {
                var template = _tempaltesFactory.GetTemplate(RandomTemplate());
                match.PendingQuestions.Add(template.Accept(_questionGenerator));
            }

            return match;
        }

        public Type RandomTemplate()
        {
            Type templateType;

            Type func()
            {
                int random = _random.Next(_tempaltesFactory.TemplateTypes.Count);
                return _tempaltesFactory.TemplateTypes[random];
            }

            do
            {
                templateType = func();
            } while (_typeQuant.Count > 0 &&  _typeQuant[templateType] <= _maximumSameTypePerMatch);

            return templateType;
        }
    }

    public interface IMatchFactory
    {
        Match BuildMatch(string playerName);
    }
}
