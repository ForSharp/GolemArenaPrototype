using System;
using Fight;

namespace GameLoop
{
    public static class EventContainer
    {
        public static event EventHandler FightEvent;
        
        public static void OnFightEvent(FightEventArgs args)
        {
            FightEvent?.Invoke(null, args);
        }
        
        public static event Action PlayerCharacterCreated;

        public static void OnPlayerCharacterCreated()
        {
            PlayerCharacterCreated?.Invoke();
        }

        public static event Action<GameCharacterState> GolemStatsChanged;

        public static void OnGolemStatsChanged(GameCharacterState state)
        {
            GolemStatsChanged?.Invoke(state);
        }

        public static event Action<RoundStatistics> GolemDied;
    
        public static void OnGolemDied(RoundStatistics killer)
        {
            GolemDied?.Invoke(killer);
        }

        public static event Action<GameCharacterState> WinBattle;

        public static void OnWinBattle(GameCharacterState obj)
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