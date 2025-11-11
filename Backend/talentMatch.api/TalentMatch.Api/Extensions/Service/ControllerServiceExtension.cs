using FluentValidation.AspNetCore;
using TalentMatch.Infrastructure.Middlewares;

namespace TalentMatch.Api.Extensions.Service
{
    public static class ControllerServiceExtension
    {
        [Obsolete("Este metodo se considera obsoleto por el ID, pero es necesario para la constucción de validators")]
        public static void AddControllerExtension(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateFilterAttribute>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            })
            .AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });
        }
    }
}