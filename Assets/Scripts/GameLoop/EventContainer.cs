using System;
using CharacterEntity.CharacterState;

namespace GameLoop
{
    public static class EventContainer
    {
        public static event EventHandler FightEvent;
        
        public static void OnFightEvent(object sender, FightEventArgs args)
        {
            FightEvent?.Invoke(sender, args);
        }

        public static event Action<CharacterState, CharacterState, float, bool> MagicDamageReceived;

        public static void OnMagicDamageReceived(CharacterState sender, CharacterState target, float damage,
            bool isPeriodic)
        {
            MagicDamageReceived?.Invoke(sender, target, damage, isPeriodic);
        }

        public static event Action PlayerCharacterCreated;

        public static void OnPlayerCharacterCreated()
        {
            PlayerCharacterCreated?.Invoke();
        }

        public static event Action<CharacterState> GolemStatsChanged;

        public static void OnGolemStatsChanged(CharacterState state)
        {
            GolemStatsChanged?.Invoke(state);
        }

        public static event Action<RoundStatistics> GolemDied;
    
        public static void OnGolemDied(RoundStatistics killer)
        {
            GolemDied?.Invoke(killer);
        }

        public static event Action<CharacterState> WinBattle;

        public static void OnWinBattle(CharacterState obj)
        {
            WinBattle?.Invoke(obj);
        }

        public static event Action NewRound;

        public static void OnNewRound()
        {
            NewRound?.Invoke();
        }
    }
}