using GameMasterDomain.Constants;
using GameMasterDomain.Entities;
using GameMasterDomain.Enums;
using GameMasterDomain.Exceptions;
using GameMasterDomain.Interfaces;
using GameMasterDomain.Models.ApiLogs;
using GameMasterDomain.Models.Envelopes;
using GameMasterDomain.Models.Responses.Ability;
using MicroservicesLogger.Interfaces;
using MicroservicesLogger.Models;
using MongoDb.Interfaces;

namespace GameMasterApplication.Services
{
    public class AbilityService : IAbilityService
    {
        private readonly IApiLog<ApiLogModel> _logger;
        private readonly IAbilityRepository _abilityRepository;
        //private readonly ISugestionRepository _sugestionRepository;

        public AbilityService(IApiLog<ApiLogModel> logger, IAbilityRepository abilityRepository)//, ISugestionRepository sugestionRepository)
        {
            _logger = logger;
            _abilityRepository = abilityRepository;
            //_sugestionRepository = sugestionRepository;
        }

        public Task<IResponse<List<AbilityDto>>> GetAbilitiesByIdAsync(Guid abilityId)
        {
            throw new NotImplementedException();
        }


        public async Task<IResponse<List<AbilityDto>>> GetAbilitiesByClassAsync(ClassType classType)
        {
            var log = await _logger.GetBaseLogAsync();
            var subLog = new SubLog();

            try
            {
                var abilities = await _abilityRepository.GetAbilitiesByClassAsync(classType);

                subLog.StartCronometer();

                if (abilities is null)
                {
                    var response = new ResponseError<List<AbilityDto>>(
                        ErrorDictionary.NotFoundError("No abilities found for the specified class."));

                    return response;
                }

                return new ResponseOk<List<AbilityDto>>(Ability.AbilityListToDto(abilities!));
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex,
                    ErrorDictionary.ServiceError("Error occur while get abilities by class name. Try again later."));
            }
            finally
            {
                subLog.StopCronometer();
                await log.AddStepAsync("SERVICE_GET_ABILITIES_BY_CLASS", subLog);
            }
        }

        public async Task<IResponse<List<AbilityDto>>> GetAbilitiesByNameAsync(string abilityName)
        {
            var log = await _logger.GetBaseLogAsync();
            var subLog = new SubLog();

            try
            {
                var abilities = await _abilityRepository.GetAbilitiesByNameAsync(abilityName);

                subLog.StartCronometer();

                if (abilities is null)
                {
                    var response = new ResponseError<List<AbilityDto>>(
                        ErrorDictionary.NotFoundError("No abilities found for the specified name."));

                    return response;
                }

                return new ResponseOk<List<AbilityDto>>(Ability.AbilityListToDto(abilities!));
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex,
                    ErrorDictionary.ServiceError("Error occur while get abilities by name. Try again later."));
            }
            finally
            {
                subLog.StopCronometer();
                await log.AddStepAsync("SERVICE_GET_ABILITIES_BY_NAME", subLog);
            }
        }

        public async Task<IResponse<List<AbilityDto>>> GetAllAbilitiesAsync(int page, int itensPage)
        {
            var log = await _logger.GetBaseLogAsync();
            var subLog = new SubLog();

            try
            {
                int skip = (page - 1) * itensPage;
                var abilities = _abilityRepository.GetAllAbilities(skip, itensPage);

                subLog.StartCronometer();

                if (abilities is null)
                {
                    var response = new ResponseError<List<AbilityDto>>(
                        ErrorDictionary.NotFoundError("No abilities found for this paginated search."));

                    return response;
                }

                return new ResponseOk<List<AbilityDto>>(Ability.AbilityListToDto(abilities!));
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex,
                    ErrorDictionary.ServiceError("Error occur while get abilities paginated. Try again later."));
            }
            finally
            {
                subLog.StopCronometer();
                await log.AddStepAsync("SERVICE_GET_ALL_ABILITIES_PAGINATED", subLog);
            }
        }

        //public async Task NewAbilitySugestion(Sugestion sugestion)
        //{
        //    await _sugestionRepository.InsertSugestionAsync(sugestion);
        //}
    }
}