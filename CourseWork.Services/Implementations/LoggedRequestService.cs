using System.IO;
using CourseWork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CourseWork.Services.Implementations
{
    /// <summary>
    /// Представляет логгированные ответы сервера.
    /// </summary>
    public class LoggedRequestService : ILoggedRequestsService
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LoggedRequestService"/>.
        /// </summary>
        public LoggedRequestService()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        /// <inheritdoc cref="ILoggedRequestsService.BadLoggedRequest"/>
        public BadRequestObjectResult BadLoggedRequest(string message)
        {
            Log.Logger.Error(message);
            return new BadRequestObjectResult(message);
        }
    }
}
