using TalentMatch.Infrastructure.Exceptions;

namespace TalentMatch.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //Log.Error($"Exception: {ex.Message} {ex.StackTrace}");

                //Log.Fatal(ex, "Host terminated unexpectedly");
                throw new CoreException($"Exception: {ex.Message} {ex.StackTrace}");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); // Usamos Startup.cs para la configuración de servicios y middleware
                });
    }
}