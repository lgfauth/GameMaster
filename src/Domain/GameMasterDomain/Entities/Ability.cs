using GameMasterDomain.Enums;

namespace GameMasterDomain.Entities
{
    public class Ability
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ManaCost { get; set; }
        public AbilityType Type { get; set; }
        public int EffectValue { get; set; }
        public int Range { get; set; }
        public int Duration { get; set; }
        public int Cooldown { get; set; }
        public int? StaminaCost { get; set; }
        public int? HealthCost { get; set; }
        public int RequiredLevel { get; set; }
        public ClassType? RequiredClass { get; set; }
        public EffectTypes? EffectType { get; set; }
        public AttributeType ScalingAttribute { get; set; }
    }
}
