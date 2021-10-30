using System;

namespace GolemEntity.ExtraStats
{
    [Serializable]
    public class GolemExtraStats
    {
        public float attackRange;
        public float attackSpeed;
        public float avoidChance;
        public float damagePerHeat;
        public float defence;
        public float dodgeChance;
        public float health;
        public float hitAccuracy;
        public float magicAccuracy;
        public float magicPower;
        public float magicResistance;
        public float manaPool;
        public float moveSpeed;
        public float regenerationHealth;
        public float regenerationMana;
        public float regenerationStamina;
        public float stamina;

        public static GolemExtraStats operator +(GolemExtraStats statsA, GolemExtraStats statsB)
        {
            return new GolemExtraStats ()
            {
                attackRange = statsA.attackRange + statsB.attackRange,
                attackSpeed = statsA.attackSpeed + statsB.attackSpeed,
                avoidChance = statsA.avoidChance + statsB.avoidChance,
                damagePerHeat = statsA.damagePerHeat + statsB.damagePerHeat,
                defence = statsA.defence + statsB.defence,
                dodgeChance = statsA.dodgeChance + statsB.dodgeChance,
                health = statsA.health + statsB.health,
                hitAccuracy = statsA.hitAccuracy + statsB.hitAccuracy,
                magicAccuracy = statsA.magicAccuracy + statsB.magicAccuracy,
                magicPower = statsA.magicPower + statsB.magicPower,
                magicResistance = statsA.magicResistance + statsB.magicResistance,
                manaPool = statsA.manaPool + statsB.manaPool,
                moveSpeed = statsA.moveSpeed + statsB.moveSpeed,
                regenerationHealth = statsA.regenerationHealth + statsB.regenerationHealth,
                regenerationMana = statsA.regenerationMana + statsB.regenerationMana,
                regenerationStamina = statsA.regenerationStamina + statsB.regenerationStamina,
                stamina = statsA.stamina + statsB.stamina
            };
        }

        public static GolemExtraStats operator *(GolemExtraStats stats, float multiplier)
        {
            return new GolemExtraStats()
            {
                attackSpeed = stats.attackSpeed * multiplier,
                avoidChance = stats.avoidChance * multiplier,
                damagePerHeat = stats.damagePerHeat * multiplier,
                defence = stats.defence * multiplier,
                dodgeChance = stats.dodgeChance * multiplier,
                health = stats.health * multiplier,
                hitAccuracy = stats.hitAccuracy * multiplier,
                magicAccuracy = stats.magicAccuracy * multiplier,
                magicPower = stats.magicPower * multiplier,
                magicResistance = stats.magicResistance * multiplier,
                manaPool = stats.manaPool * multiplier,
                moveSpeed = stats.moveSpeed * multiplier,
                regenerationHealth = stats.regenerationHealth * multiplier,
                regenerationMana = stats.regenerationMana * multiplier,
                regenerationStamina = stats.regenerationStamina * multiplier,
                stamina = stats.stamina * multiplier
            };
        }

        public override string ToString()
        {
            return $"AttackRange = {attackRange}," +
                   $"AttackSpeed = {attackSpeed}," +
                   $"AvoidChance = {avoidChance}," +
                   $"DamagePerHeat = {damagePerHeat}," +
                   $"Defence = {defence}," +
                   $"DodgeChance = {dodgeChance}," +
                   $"Health = {health}," +
                   $"HitAccuracy = {hitAccuracy}," +
                   $"MagicAccuracy = {magicAccuracy}," +
                   $"MagicDamage = {magicPower}," +
                   $"MagicResistance = {magicResistance}," +
                   $"ManaPool = {manaPool}," +
                   $"MoveSpeed = {moveSpeed}," +
                   $"RegenerationHealth = {regenerationHealth}," +
                   $"RegenerationMana = {regenerationMana}," +
                   $"RegenerationStamina = {regenerationStamina}," +
                   $"Stamina = {stamina}";
        }
    }
}
