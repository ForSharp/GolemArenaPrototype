using UnityEngine;

namespace __Scripts.GolemEntity.ExtraStats
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
            return Mathf.Max(strength, agility, intelligence) * 10;
        }
        public static float GetMoveSpeed(float strength, float agility)
        {
            //min 100, max 300 without buffs
            
            return 0;
        }
        public static float GetAttackSpeed(float agility)
        {
            return 0;
        }
        public static float GetDefence(float strength, float agility)
        {
            return (strength * 0.8f + agility * 0.2f) * 10;
        }
        public static float GetMagicDamage(float intelligence)
        {
            return 0;
        }
        public static float GetManaPool(float intelligence)
        {
            return 0;
        }
        public static float GetAvoidChance(float strength, float agility)
        {
            return 0;
        }
        public static float GetDodgeChance(float agility, float intelligence)
        {
            return 0;
        }
        public static float GetMagicResistance(float strength, float intelligence)
        {
            return 0;
        }
        public static float GetHitAccuracy(float strength, float agility)
        {
            return 0;
        }
        public static float GetMagicAccuracy(float strength, float agility)
        {
            return 0;
        }
        public static float GetRegenerationRate(float strength, float agility)
        {
            return 0;
        }
    }
}
