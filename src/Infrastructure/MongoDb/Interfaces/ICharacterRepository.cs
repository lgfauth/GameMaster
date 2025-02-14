using GameMasterDomain.Entities;

namespace MongoDb.Interfaces
{
    public interface ICharacterRepository
    {
        Task<Character?> GetByIdAsync(Guid id);
        Task CreateAsync(Character character);
        Task UpdateAsync(Character character);
        Task DeleteAsync(Guid id);
    }
}