using Microsoft.Extensions.DependencyInjection;
using TalentMatch.Core.Features.Services;
using TalentMatch.Core.Interfaces.Pagination;
using TalentMatch.Core.Interfaces.Services;

namespace TalentMatch.Core
{
    public static class ServiceRegistration
    {
        public static void AddCoreLayer(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJobSeekerService, JobSeekerService>();
            services.AddTransient<IEmployerService, EmployerService>();
            services.AddTransient<IJobPostingService, JobPostingService>();
            services.AddTransient<IApplicationsService, ApplicationsService>();
            services.AddTransient<IMatchingService, MatchingService>();
            services.AddTransient<IPagedList, PagedList>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}