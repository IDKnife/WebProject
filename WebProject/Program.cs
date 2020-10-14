using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CourseWork.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((webHost, conf) =>
                {
                    conf.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
                })
                .UseSerilog((webHost, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(webHost.Configuration);
                });
    }
}
