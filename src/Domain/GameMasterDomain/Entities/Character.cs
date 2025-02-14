using GameMasterDomain.Enums;

namespace GameMasterDomain.Entities
{
    public class Character
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public RaceType Race { get; set; }
        public ClassType Class { get; set; }
        public string Description { get; set; } = string.Empty;

        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;
        public int HealthPoints { get; set; } = 100;
        public int ManaPoints { get; set; } = 50;

        public Attribute Stats { get; set; } = new Attribute();
        public List<Item> Inventory { get; set; } = new List<Item>();
        public int Gold { get; set; } = 0;
        public List<Ability> Abilities { get; set; } = new List<Ability>();
        public List<EffectTypes> StatusEffects { get; set; } = new List<EffectTypes>();
        public string Location { get; set; } = "Starting Village";
    }
}
