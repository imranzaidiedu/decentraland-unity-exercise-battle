public abstract partial class UnitBase
{
    public class Properties
    {
        public float health { get; set; }
        public float defense { get; set; }
        public float attack { get; set; }
        public float maxAttackCooldown { get; set; }
        public float postAttackDelay { get; set; }
        public float speed { get; set; } = 0.1f;
        public float attackRange { get; set; }
    }
}


public enum UnitType
{
    WARRIOR = 0,
    ARCHER
}
