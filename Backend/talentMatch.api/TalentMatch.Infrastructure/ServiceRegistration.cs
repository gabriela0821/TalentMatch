using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TalentMatch.Core.Interfaces.Repositories;
using TalentMatch.Infrastructure.Repositories;
using TalentMatch.Infrastructure.Settings;

namespace TalentMatch.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Context>(options =>
            options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}