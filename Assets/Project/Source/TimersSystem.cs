using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimersSystem
{
    private List<TimerData> _timers = new();

    public void Tick()
    {
        if (_timers.Count == 0) return;
        
        var time = Time.time;

        for (var i = 0; i < _timers.Count; i++)
        {
            var t = _timers[i];
            if (time >= t.startTime + t.delay)
            {
                t.onEnd.Invoke();

                if (!t.repeat)
                {
                    _timers.RemoveAt(i);
                    continue;
                }

                _timers[i] = new TimerData(t.name, Time.time, t.delay, t.onEnd, t.repeat);
            }
        }
    }

    public void Start(string name, float delay, Action onEnd, bool repeat = false)
    {
        if (Contains(name)) return;
        _timers.Add(new TimerData(name, Time.time, delay, onEnd, repeat));
    }

    public void Stop(string name) => _timers.RemoveAll(t => t.name == name);
    public void StopAll() => _timers.Clear();
    public bool Contains(string name) => _timers.Any(t => t.name == name);

    private struct TimerData
    {
        public TimerData(string name, float startTime, float delay, Action onEnd, bool repeat)
        {
            this.name = name;
            this.startTime = startTime;
            this.delay = delay;
            this.onEnd = onEnd;
            this.repeat = repeat;
        }

        public readonly string name;
        public readonly float startTime;
        public readonly float delay;
        public readonly Action onEnd;
        public readonly bool repeat;
    }
}