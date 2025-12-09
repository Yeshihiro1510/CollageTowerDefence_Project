using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] private UI _UI;
    [SerializeField] private SplineContainer _lineContainer;
    [SerializeField] private EnemyView _enemyPrefab;
    [SerializeField] private Transform _enemySpawn;
    [SerializeField] private Transform _target;

    private readonly ResourcesSystem _resourcesService = new ResourcesSystem();
    private readonly TimersSystem _timersService = new TimersSystem();

    private float _time = 0f;
    private float _timeScale = 1f;
    private float _deltaTime = 0f;
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

    private void OnEnable() => _paused = false;
    private void OnDisable() => _paused = true;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif

        if (_paused) return;
        _deltaTime = Time.deltaTime * _timeScale;
        _time += _deltaTime;

        _timersService.Tick(_time);
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
        TimerData timerData = _timersService.Run(displayText, delay,
            repeat ? onEnd : onEnd + (_ => timersMenu.Remove(timerView)),
            onTick + timerView.Display, repeat);
    }

    public void NewButton(string displayText, UnityAction onClick)
    {
        Button button = _UI.Get<ButtonMenuView>().Draw(displayText, onClick);
    }

    public EnemyView NewEnemy(float delay, Action<EnemyView> onEnd, Action<EnemyView> onTick, bool repeat = false)
    {
        var enemy = SinglePool.Instance.Get(_enemyPrefab);
        enemy.transform.position = _enemySpawn.position;

        _timersService.Run("Enemy steps",
            delay,
            t => onEnd?.Invoke(enemy),
            t =>
            {
                onTick?.Invoke(enemy);
                enemy.transform.position = Vector3.Lerp(_enemySpawn.position, _target.position, t.progress);
            },
            repeat);

        return enemy;
    }
}