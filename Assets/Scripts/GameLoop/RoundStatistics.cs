using CharacterEntity.CharacterState;
using CharacterEntity.State;

namespace GameLoop
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
        public bool WinLastRound { get; set; } = false;

        public RoundStatistics(CharacterState owner)
        {
            Owner = owner;
        }
    }
}