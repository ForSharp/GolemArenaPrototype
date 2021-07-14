namespace GolemEntity.ExtraStats
{
    public class GolemExtraStats 
    {
        public float AttackRange { get; set; }
        public float AttackSpeed { get; set; } 
        public float AvoidChance { get; set; }
        public float DamagePerHeat { get; set; }
        public float Defence { get; set; }
        public float DodgeChance { get; set; }
        public float Health { get; set; }
        public float HitAccuracy { get; set; }
        public float MagicAccuracy { get; set; }
        public float MagicDamage { get; set; }
        public float MagicResistance { get; set; }
        public float ManaPool { get; set; }
        public float MoveSpeed { get; set; }
        public float RegenerationHealth { get; set; }
        public float RegenerationMana { get; set; }
        public float RegenerationStamina { get; set; }
        public float Stamina { get; set; }
        
        
        public static GolemExtraStats operator +(GolemExtraStats statsA, GolemExtraStats statsB)
        {
            return new GolemExtraStats ()
            {
                AttackRange = statsA.AttackRange + statsB.AttackRange,
                AttackSpeed = statsA.AttackSpeed + statsB.AttackSpeed,
                AvoidChance = statsA.AvoidChance + statsB.AvoidChance,
                DamagePerHeat = statsA.DamagePerHeat + statsB.DamagePerHeat,
                Defence = statsA.Defence + statsB.Defence,
                DodgeChance = statsA.DodgeChance + statsB.DodgeChance,
                Health = statsA.Health + statsB.Health,
                HitAccuracy = statsA.HitAccuracy + statsB.HitAccuracy,
                MagicAccuracy = statsA.MagicAccuracy + statsB.MagicAccuracy,
                MagicDamage = statsA.MagicDamage + statsB.MagicDamage,
                MagicResistance = statsA.MagicResistance + statsB.MagicResistance,
                ManaPool = statsA.ManaPool + statsB.ManaPool,
                MoveSpeed = statsA.MoveSpeed + statsB.MoveSpeed,
                RegenerationHealth = statsA.RegenerationHealth + statsB.RegenerationHealth,
                RegenerationMana = statsA.RegenerationMana + statsB.RegenerationMana,
                RegenerationStamina = statsA.RegenerationStamina + statsB.RegenerationStamina,
                Stamina = statsA.Stamina + statsB.Stamina
            };
        }

        public static GolemExtraStats operator *(GolemExtraStats stats, float multiplier)
        {
            return new GolemExtraStats()
            {
                AttackSpeed = stats.AttackSpeed * multiplier,
                AvoidChance = stats.AvoidChance * multiplier,
                DamagePerHeat = stats.DamagePerHeat * multiplier,
                Defence = stats.Defence * multiplier,
                DodgeChance = stats.DodgeChance * multiplier,
                Health = stats.Health * multiplier,
                HitAccuracy = stats.HitAccuracy * multiplier,
                MagicAccuracy = stats.MagicAccuracy * multiplier,
                MagicDamage = stats.MagicDamage * multiplier,
                MagicResistance = stats.MagicResistance * multiplier,
                ManaPool = stats.ManaPool * multiplier,
                MoveSpeed = stats.MoveSpeed * multiplier,
                RegenerationHealth = stats.RegenerationHealth * multiplier,
                RegenerationMana = stats.RegenerationMana * multiplier,
                RegenerationStamina = stats.RegenerationStamina * multiplier,
                Stamina = stats.Stamina * multiplier
            };
        }

        public override string ToString()
        {
            return $"AttackRange = {AttackRange}," +
                   $"AttackSpeed = {AttackSpeed}," +
                   $"AvoidChance = {AvoidChance}," +
                   $"DamagePerHeat = {DamagePerHeat}," +
                   $"Defence = {Defence}," +
                   $"DodgeChance = {DodgeChance}," +
                   $"Health = {Health}," +
                   $"HitAccuracy = {HitAccuracy}," +
                   $"MagicAccuracy = {MagicAccuracy}," +
                   $"MagicDamage = {MagicDamage}," +
                   $"MagicResistance = {MagicResistance}," +
                   $"ManaPool = {ManaPool}," +
                   $"MoveSpeed = {MoveSpeed}," +
                   $"RegenerationHealth = {RegenerationHealth}," +
                   $"RegenerationMana = {RegenerationMana}," +
                   $"RegenerationStamina = {RegenerationStamina}," +
                   $"Stamina = {Stamina}";
        }
    }
}
