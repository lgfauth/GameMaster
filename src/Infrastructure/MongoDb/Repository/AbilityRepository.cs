﻿using GameMasterDomain.Entities;
using GameMasterDomain.Enums;
using Microsoft.Extensions.Options;
using MongoDb.Interfaces;
using MongoDb.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace MongoDb.Repository
{
    /// <summary>
    /// Repository for Abities, always for search then.
    /// </summary>
    public class AbilityRepository : IAbilityRepository
    {
        private readonly IMongoCollection<Ability> _abilities;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mongoDbSettings">Settgins for MongoDB connection.</param>
        /// <param name="mongoDbData">Data for MongoDB connection.</param>
        public AbilityRepository(IOptions<MongoDbSettings> mongoDbSettings, IOptions<MongoDbData> mongoDbData)
        {
            string connectionString = string.Format(
                mongoDbSettings.Value.ConnectionString,
                mongoDbData.Value.user,
                mongoDbData.Value.passsword,
                mongoDbData.Value.cluster);

            var client = new MongoClient(connectionString);

            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _abilities = database.GetCollection<Ability>("Ability");
        }

        public async Task<IEnumerable<Ability?>> GetAbilitiesByClassAsync(ClassType classType)
        {
            var filter = Builders<Ability>.Filter.Regex(a =>
                a.RequiredClass, new BsonRegularExpression($".*{classType}.*", "i"));

            var response = await _abilities.FindAsync(filter);

            return response.ToEnumerable().OrderBy(x => x.RequiredLevel);
        }

        public async Task<IEnumerable<Ability?>> GetAbilitiesByNameAsync(string abilityName)
        {
            var escapedName = Regex.Escape(abilityName);
            var filter = Builders<Ability>.Filter.Regex(a =>
                a.Name, new BsonRegularExpression($".*{escapedName}.*", "i"));

            var response = await _abilities.FindAsync(filter);

            return response.ToEnumerable().OrderBy(x => x.RequiredLevel);
        }

        public IEnumerable<Ability?> GetAllAbilities(int skip, int itensPage)
        {
            var filter = Builders<Ability>.Filter.Empty;

            var response = _abilities.Find(filter).Skip(skip).Limit(itensPage).ToEnumerable();

            return response.OrderBy(x => x.RequiredLevel);
        }
    }
}
