using UnityEngine;

namespace __Scripts.ExtraStats
{
    /// <summary>
    /// All methods returns the maximum allowed half,
    /// then the GolemType values and GolemSpecialization values are concatenated.
    /// </summary>
    public static class ExtraStatsCalculator 
    {
        
        public static float GetHealth(float strength)
        {
            return strength * 10;
        }
        public static float GetStamina(float strength, float agility)
        {
            return (agility * 0.8f + strength * 0.2f) * 10;
        }
        public static float GetDamagePerHeat(float strength, float agility, float intelligence)
        {
            return (strength + agility + intelligence);
        }
        public static float GetMoveSpeed()
        {
            return 0;
        }
        public static float GetAttackSpeed()
        {
            return 0;
        }
        public static float GetDefence()
        {
            return 0;
        }
        public static float GetMagicDamage()
        {
            return 0;
        }
        public static float GetManaPool()
        {
            return 0;
        }
        public static float GetAvoidChance()
        {
            return 0;
        }
        public static float GetDodgeChance()
        {
            return 0;
        }
        public static float GetMagicResistance()
        {
            return 0;
        }
        public static float GetHitAccuracy()
        {
            return 0;
        }
        public static float GetMagicAccuracy()
        {
            return 0;
        }
        public static float GetRegenerationRate()
        {
            return 0;
        }
    }
}
