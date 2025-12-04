using System;
using System.Collections;

public abstract class GameplayScript
{
    public Main Main { get; set; }
    public UI UI { get; set; }
    public TimersSystem Timers { get; set; }
    public ResourcesSystem Resources { get; set; }
    public Func<bool> IsPaused { get; set; }
    public abstract IEnumerator Script();
}