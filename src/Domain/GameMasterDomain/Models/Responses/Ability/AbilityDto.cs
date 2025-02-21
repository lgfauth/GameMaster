using GameMasterDomain.Entities;

namespace GameMasterDomain.Models.Responses.Ability
{
    /// <summary>
    /// Object returned for abilities requests.
    /// </summary>
    public record AbilityDto
    {
        /// <summary>
        /// Name of ability.
        /// </summary>
        /// <example>Piercing Roar</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description of ability.
        /// </summary>
        /// <example>Emits a terrifying roar that lowers the defense of nearby enemies.</example>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Cost of mana for ability.
        /// </summary>
        /// <example>0</example>
        public int ManaCost { get; set; }

        /// <summary>
        /// The value of effect, can be a squaremeter or turns.
        /// </summary>
        /// <example>4</example>
        public int EffectValue { get; set; }

        /// <summary>
        /// A range of ability, can be a squaremeter or meter or miles.
        /// </summary>
        /// <example>4</example>
        public int Range { get; set; }

        /// <summary>
        /// A duration of ability, just in time or turns.
        /// </summary>
        /// <example>2</example>
        public int Duration { get; set; }

        /// <summary>
        /// A count of time (turns) need to whait to use the ability again.
        /// </summary>
        /// <example>2</example>
        public int Cooldown { get; set; }

        /// <summary>
        /// A statmina cost of ability, if it need.
        /// </summary>
        /// <example>5</example>
        public int? StaminaCost { get; set; }

        /// <summary>
        /// A health cost of ability, if it need.
        /// </summary>
        /// <example>2</example>
        public int? HealthCost { get; set; }

        /// <summary>
        /// A level required to use or adquire or learn the ability.
        /// </summary>
        /// <example>4</example>
        public int RequiredLevel { get; set; }

        /// <summary>
        /// A object of kind of effect type of this ability is.
        /// </summary>
        public EffectTypes EffectType { get; set; }

        /// <summary>
        /// A kind of type of this ability.
        /// </summary>
        /// <example>Buff</example>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// A class that is required for use or adquire or learn the ability.
        /// </summary>
        /// <example>Warrior</example>
        public string RequiredClass { get; set; } = string.Empty;

        /// <summary>
        /// A atribute that can be used like a modifier with the ability.
        /// </summary>
        /// <example>Strength</example>
        public string ScalingAttribute { get; set; } = string.Empty;
    }
}