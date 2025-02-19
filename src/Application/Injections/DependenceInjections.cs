using GameMasterApplication.Services;
using GameMasterDomain.Interfaces;
using GameMasterDomain.Models;
using MicroservicesLogger;
using MicroservicesLogger.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDb.Interfaces;
using MongoDb.Repository;
using MongoDb.Settings;

namespace GameMasterApplication.Injections
{
    public class DependenceInjections
    {
        public static void Injections(IServiceCollection service)
        {

            // Middlewares
            service.AddScoped<IApiLog<ApiLogModel>, ApiLog<ApiLogModel>>();

            // Services
            service.AddScoped<IAbilityService, AbilityService>();


            // Repositories
            service.AddScoped<IAbilityRepository, AbilityRepository>();
            service.AddScoped<ICharacterRepository, CharacterRepository>();
        }

        public static void Configurations(IServiceCollection service, IConfigurationManager configuration)
        {
            service.Configure<MongoDbData>(configuration.GetSection("MongoDbData"));
            service.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        }
    }
}
