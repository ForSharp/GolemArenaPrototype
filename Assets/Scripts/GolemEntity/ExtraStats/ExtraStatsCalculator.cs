using UnityEngine;

namespace GolemEntity.ExtraStats
{
    /// <summary>
    /// All methods returns the maximum allowed half,
    /// then the GolemType values and GolemSpecialization values are concatenated.
    /// </summary>
    public static class ExtraStatsCalculator 
    {
        public static float GetAttackRange()
        {
            return default;
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
            return (agility * 0.85f + strength * 0.15f);
        }

        public static float GetDamagePerHeat(float strength, float agility, float intelligence)
        {
            return Mathf.Max(strength, agility, intelligence) * 0.3f;
        }

        public static float GetDefence(float strength, float agility)
        {
            return (strength * 0.8f + agility * 0.2f) * 10;
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
            var speed = (200 + (agility - strength));
            if (speed > 300)
                return 300;
            if (speed < 100)
                return 100;
            return speed;
        }

        public static float GetMagicAccuracy(float strength, float intelligence)
        {
            var speed = (200 + (intelligence - strength));
            if (speed > 300)
                return 300;
            if (speed < 100)
                return 100;
            return speed;
        }

        public static float GetMagicDamage(float intelligence)
        {
            return intelligence * 0.25f;
        }

        public static float GetMagicResistance(float strength, float intelligence)
        {
            return (intelligence * 0.35f + strength * 0.65f);
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
            var speed = (200 + (strength - agility));
            if (speed > 300)
                return 300;
            if (speed < 100)
                return 100;
            return speed;
        }

        public static float GetRegenerationMana(float intelligence)
        {
            return default;
        }

        public static float GetRegenerationStamina(float agility)
        {
            return default;
        }

        public static float GetStamina(float strength, float agility)
        {
            return (agility * 0.8f + strength * 0.2f) * 10;
        }
    }
}
