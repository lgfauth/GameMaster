using GameMasterDomain.Models.Responses.Ability;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameMasterDomain.Entities
{
    public class Ability
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ManaCost { get; set; }
        public int EffectValue { get; set; }
        public int Range { get; set; }
        public int Duration { get; set; }
        public int Cooldown { get; set; }
        public int? StaminaCost { get; set; }
        public int? HealthCost { get; set; }
        public int RequiredLevel { get; set; }
        public EffectTypes EffectType { get; set; }
        public string? Type { get; set; }
        public string? RequiredClass { get; set; }
        public string? ScalingAttribute { get; set; }


        public static AbilityDto AbilityToDto(Ability ability)
        {
            var abilityDto = new AbilityDto
            {
                Name = ability.Name,
                Description = ability.Description,
                ManaCost = ability.ManaCost,
                EffectValue = ability.EffectValue,
                Range = ability.Range,
                Duration = ability.Duration,
                Cooldown = ability.Cooldown,
                StaminaCost = ability.StaminaCost,
                HealthCost = ability.HealthCost,
                RequiredLevel = ability.RequiredLevel,
                EffectType = ability.EffectType!,
                RequiredClass = ability.RequiredClass!,
                ScalingAttribute = ability.ScalingAttribute!,
                Type = ability.Type!
            };

            return abilityDto;
        }

        public static List<AbilityDto> AbilityListToDto(IEnumerable<Ability> abilities)
        {
            var abilityDtos = new List<AbilityDto>();

            foreach (var ability in abilities)
                abilityDtos.Add(AbilityToDto(ability));

            return abilityDtos;
        }
    }
}
