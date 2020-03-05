using Matches.Infrastructure.Persistence;

namespace Matches.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
         
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<TeamContext>();
                    context.Database.Migrate();

                    //ToDo bug fix
                    //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    TeamContextInitalizer.Initialize(context);
                }
                catch (Exception ex)
                {
                }


                host.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
