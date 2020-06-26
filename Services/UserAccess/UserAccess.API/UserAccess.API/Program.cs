using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace UserAccess.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                })
                .Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
        //public static void Main(string[] args)
        //{

        //    CreateHostBuilder(args).Build().Run();


        //    //try
        //    //{
        //    //    Log.Information("Starting host...");
        //    //    var host = CreateHostBuilder(args).Build();

        //    //    using (var scope = host.Services.CreateScope())
        //    //    {
        //    //        var context = scope.ServiceProvider.GetService<UserAccessContext>();
        //    //        context.Database.Migrate();

        //    //        UserAccessContextInitializer.Initialize(context);


        //    //        host.Run();
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Log.Fatal(ex, "Host terminated unexpectedly.");
        //    //}
        //    //finally
        //    //{
        //    //    Log.CloseAndFlush();
        //    //}
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args)
        //{
        //    return Host.CreateDefaultBuilder(args)
        //        .UseSerilog()
        //        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        //}
    }
}