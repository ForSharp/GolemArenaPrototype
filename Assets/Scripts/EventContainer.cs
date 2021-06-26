using System;

public static class EventContainer
{
    public static event Action GolemCreated;

    public static void OnGolemCreated()
    {
        GolemCreated?.Invoke();
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