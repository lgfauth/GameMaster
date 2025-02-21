using MicroservicesLogger.Models;

namespace GameMasterDomain.Models.ApiLogs
{
    public class ApiLogModel : LogObject
    {
        public string? Endpoint { get; set; }
    }
}