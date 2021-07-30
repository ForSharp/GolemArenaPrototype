﻿using System;

namespace GameLoop
{
    public static class EventContainer
    {
        public static event Action PlayerCharacterCreated;

        public static void OnPlayerCharacterCreated()
        {
            PlayerCharacterCreated?.Invoke();
        }

        public static event Action GolemStatsChanged;

        public static void OnGolemStatsChanged()
        {
            GolemStatsChanged?.Invoke();
        }

        public static event Action GolemDied;
    
        public static void OnGolemDied()
        {
            GolemDied?.Invoke();
        }

    }
}