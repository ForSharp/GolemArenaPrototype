using UnityEngine;

namespace GolemEntity.ExtraStats
{
    /// <summary>
    /// All methods returns the maximum allowed half,
    /// then the GolemType values and GolemSpecialization values are concatenated.
    /// </summary>
    public static class ExtraStatsCalculator 
    {
        public static float GetAttackRange(float range = 0.777f)
        {
            return range;
        }

        public static float GetAttackSpeed(float agility)
        {
            var result = agility / 3.5f;
            if (result < 40)
            {
                return 40;
            }
            if (result > 150)
            {
                return 150;
            }
            return result;
        }

        public static float GetAvoidChance(float strength, float agility)
        {
            return (agility * 0.45f + strength * 0.15f);
        }

        public static float GetDamagePerHeat(float strength, float agility, float intelligence)
        {
            return Mathf.Max(strength, agility, intelligence) * 0.6f + (strength + agility + intelligence) * 0.3f;
        }

        public static float GetDefence(float strength, float agility)
        {
            return (strength * 0.6f + agility * 0.4f);
        }

        public static float GetDodgeChance(float agility, float intelligence)
        {
            return (agility * 0.55f + intelligence * 0.45f);
        }

        public static float GetHealth(float strength)
        {
            return strength * 12;
        }

        public static float GetHitAccuracy(float strength, float agility)
        {
            return (agility * 0.55f + strength * 0.30f);
        }

        public static float GetMagicAccuracy(float strength, float intelligence)
        {
            return (intelligence * 0.65f + strength * 0.15f);
        }

        public static float GetMagicDamage(float intelligence)
        {
            return intelligence * 0.35f;
        }

        public static float GetMagicResistance(float strength, float intelligence)
        {
            return (intelligence * 0.45f + strength * 0.55f);
        }

        public static float GetManaPool(float intelligence)
        {
            return intelligence * 12;
        }

        public static float GetMoveSpeed(float strength, float agility)
        {
            var speed = (2.25f + ((agility / 100) - (strength / 100)));
            if (speed > 3)
                return 3;
            if (speed < 2)
                return 2;
            return speed;
        }

        public static float GetRegenerationHealth(float strength, float agility)
        {
            return (agility * 0.006f + strength * 0.012f);
        }

        public static float GetRegenerationMana(float intelligence)
        {
            return intelligence * 0.012f;
        }

        public static float GetRegenerationStamina(float agility)
        {
            return agility * 0.012f;
        }

        public static float GetStamina(float strength, float agility)
        {
            return (agility * 0.8f + strength * 0.2f) * 10;
        }
    }
}
