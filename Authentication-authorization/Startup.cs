using CourseWork.Repositories.Implementations;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Implementations;
using CourseWork.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;

namespace Authentication_authorization
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Mongo");
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            services.AddControllers();
            services.AddSingleton<IMongoDatabase>(client.GetDatabase(connection.DatabaseName));
            AddServices(services);
            AddRepositories(services);
            AddSwagger(services);
            AddCors(services);
        }

        private void AddServices(IServiceCollection services)
            => services
                .AddTransient<IClientService, ClientService>()
                .AddTransient<IOrderService, OrderService>()
                .AddTransient<ICreateTokenService, CreateTokenService>()
                .AddTransient<IIdentificationService, IdentificationService>();

        private void AddRepositories(IServiceCollection services)
            => services
                .AddTransient<IClientRepository, ClientRepository>()
                .AddTransient<IOrderRepository, OrderRepository>();

        private void AddSwagger(IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" });
            });

        private void AddCors(IServiceCollection services)
            => services.AddCors(o => o.AddPolicy("DefaultPolicy", builder =>
            {
                builder
                    //.WithMethods("Access-Control-Allow-Methods,GET,POST,PUT,DELETE,PATCH,OPTIONS")
                    //.WithHeaders("Access-Control-Allow-Origin,Access-Control-Allow-Methods")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("https://localhost:5011", "http://localhost:5010");
            }));

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
