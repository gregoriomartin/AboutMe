using Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.Questions.Templates.Factory
{
    public class TemplatesFactory : ITemplatesFactory
    {
        private readonly IConfiguration _configuration;
        private readonly MyInfoContext _dbContext;
        private readonly Dictionary<Type, QuestionTemplate> _templates;

        public TemplatesFactory(IConfiguration configuration,
            MyInfoContext dbContext)
        {
            QuestionTemplate.GameMasterName = configuration.GetSection("AboutMe:MyName").Value;
            _templates = new Dictionary<Type, QuestionTemplate>();
            _dbContext = dbContext;
            _configuration = configuration;
            FillTemplatesTypes();
        }

        public List<Type> TemplateTypes { get; set; }

        private void FillTemplatesTypes()
        {
            var type = typeof(QuestionTemplate);
            TemplateTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract).ToList();
        }


        public QuestionTemplate GetTemplate(Type type)
        {

            if (!_templates.TryGetValue(type, out QuestionTemplate value))
            {
                value = (QuestionTemplate)Activator.CreateInstance(type);
                if (type == typeof(ChooseOneTitleTemplate))
                {
                    if (!int.TryParse(_configuration.GetSection("GameSettings:NumberOfTitlesPerQuiz").Value, out int numberOfTitlesPerQuiz))
                        numberOfTitlesPerQuiz = 3;
                    ((ChooseOneTitleTemplate)value).SetNumberOfTitles(numberOfTitlesPerQuiz).SetTitles(_dbContext.Titles.ToList());
                }
            }

            return value;
        }
    }

    public interface ITemplatesFactory
    {
        QuestionTemplate GetTemplate(Type type);
        List<Type> TemplateTypes { get; }
    }
}
