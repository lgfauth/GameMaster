using GameMasterDomain.Entities;
using Microsoft.Extensions.Options;
using MongoDb.Interfaces;
using MongoDb.Settings;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDb.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IMongoCollection<Character> _characters;

        public CharacterRepository(IOptions<MongoDbSettings> mongoDbSettings, IOptions<MongoDbData> mongoDbData)
        {
            var client = new MongoClient(string.Format(mongoDbSettings.Value.ConnectionString, mongoDbData.Value.user, mongoDbData.Value.passsword, mongoDbData.Value.cluster));
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _characters = database.GetCollection<Character>("Characters");
        }

        public async Task<Character?> GetByIdAsync(Guid id) => await _characters.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Character character) => await _characters.InsertOneAsync(character);

        public async Task UpdateAsync(Character character) => await _characters.ReplaceOneAsync(c => c.Id == character.Id, character);

        public async Task DeleteAsync(Guid id) => await _characters.DeleteOneAsync(c => c.Id == id);
    }
}
