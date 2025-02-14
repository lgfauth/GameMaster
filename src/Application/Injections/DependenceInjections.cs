using GameMasterApplication.Models;
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
            service.AddScoped<IApiLog<ApiLogModel>, ApiLog<ApiLogModel>>();
            service.AddSingleton<ICharacterRepository, CharacterRepository>();
        }

        public static void Configurations(IServiceCollection service, IConfigurationManager configuration)
        {
            service.Configure<MongoDbData>(configuration.GetSection("MongoDbData"));
            service.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        }
    }
}
