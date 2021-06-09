using System;

public class EventContainer
{
    public event Action GolemCreated;

    public virtual void OnGolemCreated()
    {
        GolemCreated?.Invoke();
    }
}