using System.Text;
using CourseWork.Models.Core;
using CourseWork.Repositories.Implementations;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Implementations;
using CourseWork.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CourseWork.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IClientRepository, ClientRepository>()
                .AddTransient<IOrderRepository, OrderRepository>();

        public static IServiceCollection AddServices(this IServiceCollection services)
            => services
                .AddTransient<IProductService, ProductService>()
                .AddTransient<IClientService, ClientService>()
                .AddTransient<IOrderService, OrderService>()
                .AddTransient<IAccessService, AccessService>();

        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
                { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });

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

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Contstants.SecureKey)),
                        ValidateIssuerSigningKey = true,
                    };
                });
            return services;
        }
    }
}
