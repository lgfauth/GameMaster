using MicroservicesLogger.Models;

namespace GameMasterApplication.Models
{
    public class ApiLogModel : LogObject
    {
        public string? Endpoint { get; set; }
    }
}