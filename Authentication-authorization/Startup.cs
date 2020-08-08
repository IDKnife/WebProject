using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Repositories.Implementations;
using CourseWork.Repositories.Interfaces;
using CourseWork.Services.Implementations;
using CourseWork.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;

namespace Authentication_authorization
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Mongo");
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            services.AddControllers();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IClientService, ClientService>();
            services.AddSingleton<IMongoDatabase>(client.GetDatabase(connection.DatabaseName));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddCors(o => o.AddPolicy("DefaultPolicy", builder =>
            {
                builder
                    .WithMethods("Access-Control-Allow-Methods,GET,POST,PUT,DELETE,PATCH,OPTIONS")
                    .WithHeaders("Access-Control-Allow-Origin,Access-Control-Allow-Methods")
                    .AllowAnyHeader()
                    .WithOrigins("https://localhost:5011", "http://localhost:5010");
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
