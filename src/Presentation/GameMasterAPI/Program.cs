using GameMasterApplication.Injections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace GameMasterAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            DependenceInjections.Configurations(builder.Services, builder.Configuration);
            DependenceInjections.Injections(builder.Services);

            builder.Services.AddControllers(options =>
            {
                // Injection global restriction for consumes and produces
                options.Filters.Add(new ConsumesAttribute("application/json"));
                options.Filters.Add(new ProducesAttribute("application/json"));
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();

                var domainXmlPath = Path.Combine(AppContext.BaseDirectory, "GameMasterDomain.xml");
                var presentationXmlPath = Path.Combine(AppContext.BaseDirectory, 
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

                options.IncludeXmlComments(domainXmlPath);
                options.IncludeXmlComments(presentationXmlPath);

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Ruler RPG API", Version = "v1" });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI();
            }

            app.UseSwagger();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
