using GameCore.Questions;
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
            _numberOfQuestions = result;
        }

        public Match BuildMatch(string playerName)
        {
            Match match = new Match(_viewFinder)
            {
                Player = new Domain.Entities.Player
                {
                    Name = playerName,
                    Score = 0
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

            int random;
            do
            {
                random = _random.Next(_tempaltesFactory.TemplateTypes.Count);
                templateType = _tempaltesFactory.TemplateTypes[random];

            } while (_typeQuant.Count(tq => tq.Key == templateType) > 0 &&  _typeQuant[templateType] <= _maximumSameTypePerMatch);

            if(!_typeQuant.TryGetValue(templateType, out int val)){
                val = 0;
            }
            _typeQuant.Add(templateType, ++val);

            return templateType;
        }
    }

    public interface IMatchFactory
    {
        Match BuildMatch(string playerName);
    }
}
