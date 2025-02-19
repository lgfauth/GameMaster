using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameMasterDomain.Entities
{
    public class EffectTypes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// A name of effect type.
        /// </summary>
        /// <example>Buff</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description of effect.
        /// </summary>
        /// <example>Utility effects are those that provide a benefit to the character, but do not directly affect combat.</example>
        public string Effect { get; set; } = string.Empty;

        /// <summary>
        /// Maybe a duration of the effect, if that need.
        /// </summary>
        /// <example>2</example>
        public int Duration { get; set; }
    }
}
