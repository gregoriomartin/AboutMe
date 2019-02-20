using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class IoCExtension
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddDbContext<MyInfoContext>();
        }
    }
}
