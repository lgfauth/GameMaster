using MicroservicesLogger.Models;

namespace GameMasterDomain.Models
{
    public class ApiLogModel : LogObject
    {
        public string? Endpoint { get; set; }
    }
}