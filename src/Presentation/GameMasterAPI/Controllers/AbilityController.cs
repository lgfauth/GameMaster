using GameMasterDomain.Enums;
using GameMasterDomain.Exceptions;
using GameMasterDomain.Interfaces;
using GameMasterDomain.Models;
using GameMasterDomain.Models.Responses;
using GameMasterDomain.Models.Sugestions;
using MicroservicesLogger.Enums;
using MicroservicesLogger.Interfaces;
using MicroservicesLogger.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameMasterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbilityController : ControllerBase
    {
        private readonly IApiLog<ApiLogModel> _logger;
        private readonly IAbilityService _abilityService;

        public AbilityController(IApiLog<ApiLogModel> logger, IAbilityService abilityService)
        {
            _logger = logger;
            _abilityService = abilityService;
        }


        [HttpGet("byClass/{className}")]
        [ProducesResponseType(typeof(ErrorObject), 400)]
        [ProducesResponseType(typeof(ErrorObject), 404)]
        [ProducesResponseType(typeof(ErrorObject), 500)]
        [ProducesResponseType(typeof(List<AbilityDto>), 200)]
        public async Task<IActionResult> GetAbilitiesByClassAsync(string className)
        {
            var log = await _logger.CreateBaseLogAsync();
            var sublog = new SubLog();

            try
            {
                log.Request = new { className };

                Enum.TryParse<ClassType>(className, true, out var selectedClass);

                var abilities = await _abilityService.GetAbilitiesByClassAsync(selectedClass);

                log.Response = new { abilities.Data, abilities.IsSuccess };

                if (abilities is null || !abilities!.IsSuccess)
                    return NotFound(abilities);

                return Ok(abilities);
            }
            catch (InternalErrorException iex)
            {
                log.Level = LogTypes.WARN;
                sublog.Exception = iex.InnerException;
                log.Response = iex.Error;

                return StatusCode(400, iex.Error);
            }
            catch (Exception ex)
            {
                log.Level = LogTypes.ERROR;
                sublog.Exception = ex;
                var response = new ErrorObject { Details = "An error occurred while retrieving abilities by class.", ErrorCode = "" };
                log.Response = response;

                return StatusCode(500, response);
            }
            finally
            {
                log.Level = LogTypes.INFO;
                await log.AddStepAsync("CONTROLLER_GET_ABILITIES_BY_CLASS", sublog);
                await _logger.WriteLogAsync(log);
            }
        }

        // GET: api/ability/byName/{abilityName}
        [HttpGet("byName/{abilityName}")]
        public async Task<IActionResult> GetAbilitiesByNameAsync(string abilityName)
        {
            var log = await _logger.CreateBaseLogAsync();
            var sublog = new SubLog();

            try
            {
                var abilities = await _abilityService.GetAbilitiesByNameAsync(abilityName);
                if (abilities is null || !abilities.IsSuccess)
                    return NotFound("No abilities found with the specified name.");

                return Ok(abilities);
            }
            catch (InternalErrorException iex)
            {
                sublog.Exception = iex.InnerException;
                log.Response = iex.Error;

                return StatusCode(400, iex.Error);
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                var response = new ErrorObject { Details = "An error occurred while retrieving abilities by class.", ErrorCode = "" };
                log.Response = response;

                return StatusCode(500, response);
            }
            finally
            {
                await log.AddStepAsync("CONTROLLER_GET_ABILITIES_BY_NAME", sublog);
                await _logger.WriteLogAsync(log);
            }
        }

        // GET: api/ability?page=1&itensPage=10
        [HttpGet]
        public async Task<IActionResult> GetAllAbilitiesAsync([FromQuery] int page, [FromQuery] int itensPage)
        {
            var log = await _logger.CreateBaseLogAsync();
            var sublog = new SubLog();

            try
            {
                var abilities = await _abilityService.GetAllAbilitiesAsync(page, itensPage);
                return Ok(abilities);
            }
            catch (InternalErrorException iex)
            {
                sublog.Exception = iex.InnerException;
                log.Response = iex.Error;

                return StatusCode(400, iex.Error);
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                var response = new ErrorObject { Details = "An error occurred while retrieving abilities by class.", ErrorCode = "" };
                log.Response = response;

                return StatusCode(500, response);
            }
            finally
            {
                await log.AddStepAsync("CONTROLLER_GET_ALL_ABILITIES", sublog);
                await _logger.WriteLogAsync(log);
            }
        }

        // POST: api/ability/sugestion
        [HttpPost("sugestion")]
        public async Task<IActionResult> NewAbilitySugestion([FromBody] AbilitySugestionRequest sugestion)
        {
            var log = await _logger.CreateBaseLogAsync();
            var sublog = new SubLog();

            try
            {
                if (sugestion == null)
                    return BadRequest("Sugestion cannot be null.");

                //await _abilityService.NewAbilitySugestion(sugestion);
                return Ok("Ability suggestion submitted successfully.");
            }
            catch (InternalErrorException iex)
            {
                sublog.Exception = iex.InnerException;
                log.Response = iex.Error;

                return StatusCode(400, iex.Error);
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                var response = new ErrorObject { Details = "An error occurred while retrieving abilities by class.", ErrorCode = "" };
                log.Response = response;

                return StatusCode(500, response);
            }
            finally
            {
                await log.AddStepAsync("CONTROLLER_POST_ABILITY_SUGESTION", sublog);
                await _logger.WriteLogAsync(log);
            }
        }
    }
}
