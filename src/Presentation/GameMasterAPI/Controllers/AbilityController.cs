using GameMasterDomain.Enums;
using GameMasterDomain.Exceptions;
using GameMasterDomain.Interfaces;
using GameMasterDomain.Models.ApiLogs;
using GameMasterDomain.Models.Requests.Sugestions;
using GameMasterDomain.Models.Responses.Ability;
using GameMasterDomain.Models.Responses.Sugestions;
using MicroservicesLogger.Enums;
using MicroservicesLogger.Interfaces;
using MicroservicesLogger.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameMasterAPI.Controllers
{
    /// <summary>
    /// Ability Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AbilityController : ControllerBase
    {
        private readonly IApiLog<ApiLogModel> _logger;
        private readonly IAbilityService _abilityService;

        /// <summary>
        /// Ability controller.
        /// </summary>
        /// <param name="logger">Log implementation</param>
        /// <param name="abilityService">Service for abilities</param>
        public AbilityController(IApiLog<ApiLogModel> logger, IAbilityService abilityService)
        {
            _logger = logger;
            _abilityService = abilityService;
        }

        /// <summary>
        /// Find a list of abilities by class name that is required to use / learn / adquire.
        /// </summary>
        /// <param name="className">Ranger</param>
        /// <returns>List of AbilityDto</returns>
        [HttpGet("byClass/{className}")]
        [ProducesResponseType(typeof(ErrorObject), 400)]
        [ProducesResponseType(typeof(ErrorObject), 404)]
        [ProducesResponseType(typeof(ErrorObject), 500)]
        [ProducesResponseType(typeof(List<AbilityDto>), 200)]
        public async Task<IActionResult> GetAbilitiesByClassAsync(string className = "Warrior")
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
                var response = new ErrorObject { Details = "An error occurred while retrieving abilities by class.", ErrorCode = "IN500" };
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

        /// <summary>
        /// Find a list of abilities by name of abities that is contains the term of search.
        /// </summary>
        /// <param name="abilityName">Warrior</param>
        /// <returns>List of AbilityDto</returns>
        [HttpGet("byName/{abilityName}")]
        [ProducesResponseType(typeof(ErrorObject), 400)]
        [ProducesResponseType(typeof(ErrorObject), 404)]
        [ProducesResponseType(typeof(ErrorObject), 500)]
        [ProducesResponseType(typeof(List<AbilityDto>), 200)]
        public async Task<IActionResult> GetAbilitiesByNameAsync(string abilityName = "Strike")
        {
            var log = await _logger.CreateBaseLogAsync();
            var sublog = new SubLog();

            try
            {
                var abilities = await _abilityService.GetAbilitiesByNameAsync(abilityName);
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
                var response = new ErrorObject { Details = "An error occurred while retrieving abilities by name.", ErrorCode = "IN500" };
                log.Response = response;

                return StatusCode(500, response);
            }
            finally
            {
                await log.AddStepAsync("CONTROLLER_GET_ABILITIES_BY_NAME", sublog);
                await _logger.WriteLogAsync(log);
            }
        }

        /// <summary>
        /// Find a list of abilities paginated.
        /// </summary>
        /// <param name="page">1</param>
        /// <param name="itensPage">50</param>
        /// <returns>List of AbilityDto</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ErrorObject), 400)]
        [ProducesResponseType(typeof(ErrorObject), 404)]
        [ProducesResponseType(typeof(ErrorObject), 500)]
        [ProducesResponseType(typeof(List<AbilityDto>), 200)]
        public async Task<IActionResult> GetAllAbilitiesAsync([FromQuery] int page = 1, [FromQuery] int itensPage = 50)
        {
            var log = await _logger.CreateBaseLogAsync();
            var sublog = new SubLog();

            try
            {
                var abilities = await _abilityService.GetAllAbilitiesAsync(page, itensPage);
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
                var response = new ErrorObject { Details = "An error occurred while retrieving abilities paginated.", ErrorCode = "IN500" };
                log.Response = response;

                return StatusCode(500, response);
            }
            finally
            {
                await log.AddStepAsync("CONTROLLER_GET_ALL_ABILITIES", sublog);
                await _logger.WriteLogAsync(log);
            }
        }

        /// <summary>
        /// If not found an ability you want, send to us a request of a new ability.
        /// </summary>
        /// <param name="sugestion">Object with data of AbilitySugestionRequest</param>
        /// <returns></returns>
        [HttpPost("sugestion")]
        [ProducesResponseType(typeof(ErrorObject), 400)]
        [ProducesResponseType(typeof(ErrorObject), 404)]
        [ProducesResponseType(typeof(ErrorObject), 500)]
        [ProducesResponseType(typeof(List<AbilitySugestionDto>), 201)]
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
