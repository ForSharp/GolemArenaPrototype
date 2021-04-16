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

        public static float GetStamina()
        {
            return 0;
        }
        public static float GetDamagePerHeat()
        {
            return 0;
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
