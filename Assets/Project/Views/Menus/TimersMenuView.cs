using System.Collections.Generic;
using UnityEngine;
using Yeshi_Pool;

public class TimersMenuView : RectPoolable
{
    [SerializeField] private TimerView _prefab;
    
    private readonly List<TimerView> _timers = new List<TimerView>();

    public TimerView Draw(string displayText, bool reverse = false)
    {
        var timer = SinglePool.Instance.Get(_prefab);
        timer.transform.SetParent(transform);
        timer.Init(displayText, reverse);
        _timers.Add(timer);
        return timer;
    }

    public void Remove(TimerView timer)
    {
        timer.Release();
        _timers.Remove(timer);
    }
}