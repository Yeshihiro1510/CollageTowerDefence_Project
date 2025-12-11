using System;
using System.Collections.Generic;
using System.Linq;

public class TimersSystem
{
    private readonly List<TimerData> _timers = new();
    private float _time;

    public void Tick(float time)
    {
        if (_timers.Count == 0) return;
        _time = time;

        for (var i = 0; i < _timers.Count; i++)
        {
            var t = _timers[i];

            t.currentTime = time;
            t.onTick?.Invoke(t);

            if (time >= t.startTime + t.delay)
            {
                t.onEnd?.Invoke(t);

                if (t.repeat) _timers[i] = new TimerData(t.name, time, t.delay, t.onEnd, t.onTick, true);
                else _timers.RemoveAt(i);
            }
        }
    }

    public TimerData Run(string name, float delay, Action<TimerData> onEnd, Action<TimerData> onTick,
        bool repeat = false)
    {
        TimerData timer = new TimerData(name, _time, delay, onEnd, onTick, repeat);
        _timers.Add(timer);
        return timer;
    }

    public void Stop(string name) => _timers.RemoveAll(t => t.name == name);
    public void Clear() => _timers.Clear();
    public bool Contains(string name) => _timers.Any(t => t.name == name);
}

public struct TimerData
{
    public TimerData(string name, float startTime, float delay, Action<TimerData> onEnd, Action<TimerData> onTick,
        bool repeat)
    {
        this.name = name;
        this.startTime = startTime;
        this.delay = delay;
        this.onEnd = onEnd;
        this.onTick = onTick;
        this.repeat = repeat;
        currentTime = this.startTime;
    }

    public string name;
    public float startTime;
    public float currentTime;
    public float delay;
    public Action<TimerData> onEnd;
    public Action<TimerData> onTick;
    public bool repeat;
    
    public float remainingTime => delay - (currentTime - startTime);
    public float timerLifeTime => currentTime - startTime;
    public float progress => timerLifeTime.normalize(delay);
}