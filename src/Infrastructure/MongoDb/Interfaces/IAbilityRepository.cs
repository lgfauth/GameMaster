using GameMasterDomain.Entities;
using GameMasterDomain.Enums;

namespace MongoDb.Interfaces
{
    public interface IAbilityRepository
    {
        Task<IEnumerable<Ability?>> GetAbilitiesByClassAsync(ClassType classType);
        Task<IEnumerable<Ability?>> GetAbilitiesByNameAsync(string abilityName);
        IEnumerable<Ability?> GetAllAbilities(int skip, int itensPage);
        //Task NewAbilitySugestion(Sugestion sugestion);
    }
}