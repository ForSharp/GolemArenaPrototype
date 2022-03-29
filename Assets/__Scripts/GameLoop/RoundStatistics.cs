using __Scripts.CharacterEntity.State;

namespace __Scripts.GameLoop
{
    public class RoundStatistics
    {
        public float Damage { get; set; }
        public float RoundDamage { get; set; }
        public int Kills { get; set; }
        public int RoundKills { get; set; }
        public int Wins { get; set; }
        public CharacterState Owner { get; }
        public int RoundRate { get; set; }
        public bool WinLastRound { get; set; } 

        public RoundStatistics(CharacterState owner)
        {
            Owner = owner;
        }
    }
}