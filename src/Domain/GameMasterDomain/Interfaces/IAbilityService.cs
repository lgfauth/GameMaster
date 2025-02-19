using GameMasterDomain.Enums;
using GameMasterDomain.Models.Envelopes;
using GameMasterDomain.Models.Responses;

namespace GameMasterDomain.Interfaces
{
    public interface IAbilityService
    {
        Task<IResponse<List<AbilityDto>>> GetAbilitiesByIdAsync(Guid abilityId);
        Task<IResponse<List<AbilityDto>>> GetAbilitiesByClassAsync(ClassType classType);
        Task<IResponse<List<AbilityDto>>> GetAbilitiesByNameAsync(string abilityName);
        Task<IResponse<List<AbilityDto>>> GetAllAbilitiesAsync(int page, int itensPage);
        //Task NewAbilitySugestion(Sugestion sugestion);
    }
}
