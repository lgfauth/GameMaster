using GameMasterApplication.Models;
using MicroservicesLogger.Enums;
using MicroservicesLogger.Interfaces;
using MicroservicesLogger.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameMasterAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly IApiLog<ApiLogModel> _logger;

        public CharacterController(IApiLog<ApiLogModel> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            Console.WriteLine("blevers");
            var log = await _logger.CreateBaseLogAsync();
            var subLog = new SubLog();
            await log.AddStepAsync("GET_CHARACTER", subLog);

            try
            {
                log.Endpoint = "CHARACTER_GET";

                subLog.StartCronometer();

                var response = new { code = "GM0010", message = "Hello World!" };
                log.Level = LogTypes.INFO;

                return Ok(response);
            }
            catch (Exception ex)
            {
                subLog.Exception = ex;
                log.Level = LogTypes.ERROR;

                var response = new { error = "GM0012", message = ex.Message };
                log.Response = response;

                return StatusCode(500, response);
            }
            finally
            {
                subLog.StopCronometer();

                await _logger.WriteLogAsync(log);
            }
        }
    }
}
