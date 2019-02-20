using GameCore.Questions;
using GameCore.Questions.Templates.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace GameCore
{
    public static class IoCExtension
    {
        public static void AddGameCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ITemplatesFactory, TemplatesFactory>();
            services.AddScoped<IQuestionGenerator, QuestionGenerator>();
            services.AddScoped<IMatchFactory, MatchFactory>();
        }
    }
}
