using CourseWork.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CourseWork.WebApi
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
            services.AddRepositories()
                .AddServices()
                .AddSingleton<IMongoDatabase>(client.GetDatabase(connection.DatabaseName))
                .AddSwagger()
                .AddCustomCors()
                .AddCustomAuthentication();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endPoints => endPoints.MapControllers());
        }
    }
}
