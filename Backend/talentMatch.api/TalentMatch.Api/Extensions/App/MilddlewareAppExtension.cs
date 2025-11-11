using TalentMatch.Infrastructure.Middlewares;

namespace TalentMatch.Api.Extensions.App
{
    public static class MilddlewareAppExtension
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalError>();
        }
    }
}