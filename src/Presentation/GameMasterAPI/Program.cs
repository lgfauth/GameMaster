
using GameMasterApplication.Injections;
using Scalar.AspNetCore;

namespace GameMasterAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            DependenceInjections.Configurations(builder.Services, builder.Configuration);
            DependenceInjections.Injections(builder.Services);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                // Option to use swagger like a documentation
                //app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "Ruler RPG API"); });

                // Option to use scalar like a documentation
                app.MapScalarApiReference();
            }


            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
