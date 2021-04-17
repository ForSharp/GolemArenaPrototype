using UnityEngine;

namespace __Scripts.ExtraStats
{
    public static class ExtraStatsCalculator 
    {
        

        public static float GetHealth(float strength, float agility, float intelligence)
        {
            return (strength + agility + intelligence) * 10;
            //should move to struct initializer
        }

        public static float GetStamina(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetDamagePerHeat(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetMoveSpeed(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetAttackSpeed(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetDefence(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetMagicDamage(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetManaPool(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetAvoidChance(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetDodgeChance(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetMagicResistance(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetHitAccuracy(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetMagicAccuracy(float strength, float agility, float intelligence)
        {
            return 0;
        }
        public static float GetRegenerationRate(float strength, float agility, float intelligence)
        {
            return 0;
        }
    }
}
