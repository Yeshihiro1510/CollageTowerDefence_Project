using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] private UI _UI;
    [SerializeField] private Spline _line;

    private readonly ResourcesSystem _resourcesService = new ResourcesSystem();
    private readonly TimersSystem _timersService = new TimersSystem();

    private bool _paused = false;

    private void Start()
    {
        _UI.Init();

        GameplayScript script = new DefaultScript()
        {
            Main = this,
            UI = _UI,
            Timers = _timersService,
            Resources = _resourcesService,
            IsPaused = () => _paused,
        };
        StartCoroutine(MainRoutine(script));
    }

    private void OnDestroy() => SinglePool.Instance.Clear();
    private void OnEnable() => _paused = false;
    private void OnDisable() => _paused = true;

    private void Update()
    {
        if (_paused) return;

        _timersService.Tick();
    }

    private IEnumerator MainRoutine(GameplayScript script)
    {
        _UI.SetState("StartLabel");
        yield return PausedCoroutinesExtension.PausedUntil(() => Input.anyKeyDown, () => _paused);
        _UI.SetState("ResourcesMenuView", "ButtonMenuView", "TimersMenuView");
        yield return script.Script();
    }

    public ObservableProperty<int> NewResource(string name, int value)
    {
        ObservableProperty<int> resource = _resourcesService.Define(name, value);
        CounterView wheatText = _UI.Get<CounterMenuView>().Draw(name);
        resource.Bind(wheatText.Display);
        return resource;
    }

    public void NewTimer(string displayText, float delay, Action<TimerData> onEnd, Action<TimerData> onTick = null,
        bool repeat = false, bool reverse = false)
    {
        if (_timersService.Contains(displayText)) return;
        var timersMenu = _UI.Get<TimersMenuView>();
        TimerView timerView = timersMenu.Draw(displayText, reverse);
        TimerData timerData = _timersService.Run(displayText, delay, repeat ? onEnd : onEnd + (_ => timersMenu.Remove(timerView)),
            onTick + timerView.Display, repeat);
    }

    public void NewButton(string displayText, UnityAction onClick)
    {
        Button button = _UI.Get<ButtonMenuView>().Draw(displayText, onClick);
    }
}