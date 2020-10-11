using CourseWork.Repositories.Implementations;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Implementations;
using CourseWork.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Authentication_authorization.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddTransient<IClientRepository, ClientRepository>()
                .AddTransient<IOrderRepository, OrderRepository>();

        public static IServiceCollection AddServices(this IServiceCollection services)
            => services
                .AddTransient<IClientService, ClientService>()
                .AddTransient<ICreateTokenService, CreateTokenService>()
                .AddTransient<IIdentificationService, IdentificationService>();

        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" });
            });

        public static IServiceCollection AddCustomCors(this IServiceCollection services)
            => services.AddCors(o => o.AddPolicy("DefaultPolicy", builder =>
            {
                builder
                    //.WithMethods("Access-Control-Allow-Methods,GET,POST,PUT,DELETE,PATCH,OPTIONS")
                    //.WithHeaders("Access-Control-Allow-Origin,Access-Control-Allow-Methods")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("https://localhost:5011", "http://localhost:5010");
            }));
    }
}
