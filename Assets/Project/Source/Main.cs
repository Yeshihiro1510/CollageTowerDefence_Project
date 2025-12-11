using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using Yeshi_Pool;

public class Main : MonoBehaviour
{
    [SerializeField] private UI _UI;
    [SerializeField] private SplineContainer _lineContainer;
    [SerializeField] private EnemyView _enemyPrefab;
    [SerializeField] private Transform _enemySpawn;
    [SerializeField] private Transform _target;

    private readonly ResourcesSystem _resourcesService = new ResourcesSystem();
    private readonly TimersSystem _timersService = new TimersSystem();

    public float Time { get; private set; }
    public float TimeScale { get; private set; } = 1f;
    public float DeltaTime { get; private set; }
    public bool Pause { get; set; }
    public int Enemies => _enemyViews.Count;

    private readonly List<RectPoolable> _gameViews = new List<RectPoolable>();
    private readonly List<EnemyView> _enemyViews = new List<EnemyView>();

    private Coroutine _gameRoutine;

    private void Start()
    {
        _UI.Init();
        StartCoroutine(StartRoutine());
    }

    private void OnEnable() => Pause = false;
    private void OnDisable() => Pause = true;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif

        if (Pause) return;
        DeltaTime = UnityEngine.Time.deltaTime * TimeScale;
        Time += DeltaTime;

        _timersService.Tick(Time);
    }

    private IEnumerator StartRoutine()
    {
        yield return WarnForSecondsRoutine("Welcome!", 3);
        yield return MainMenuRoutine();
    }

    private IEnumerator MainMenuRoutine()
    {
        Pause = false;
        _UI.SetState("StartLabel");
        yield return WaitUntil(() => Input.anyKeyDown);
        LoadGame(new DefaultScript { Game = this });
    }

    private IEnumerator GameRoutine(GameplayScript script)
    {
        Pause = false;
        _UI.SetState("ResourcesMenuView", "ButtonMenuView", "TimersMenuView");
        yield return script.Script();
    }

    public void LoadGame(GameplayScript script) => _gameRoutine ??= StartCoroutine(GameRoutine(script));
    public void LoadMenu() => StartCoroutine(MainMenuRoutine());

    public void StopGame()
    {
        if (_gameRoutine != null)
        {
            Pause = true;
            StopCoroutine(_gameRoutine);
            ClearGame();
        }
    }

    public void ClearGame()
    {
        _timersService.Clear();
        _resourcesService.Clear();
        foreach (var v in _gameViews) v.Release();
        foreach (var v in _enemyViews) v.Release();
        _gameViews.Clear();
        _enemyViews.Clear();
    }

    public ObservableProperty<int> NewResource(string name, int value)
    {
        ObservableProperty<int> resource = _resourcesService.Define(name, value);
        CounterView view = _UI.Get<CounterMenuView>().Draw(name);
        _gameViews.Add(view);
        resource.Bind(view.Display);
        return resource;
    }

    public void NewTimer(string displayText, float delay, Action<TimerData> onEnd, Action<TimerData> onTick = null,
        bool repeat = false, bool reverse = false)
    {
        if (_timersService.Contains(displayText)) return;
        var timersMenu = _UI.Get<TimersMenuView>();
        TimerView timerView = timersMenu.Draw(displayText, reverse);
        _timersService.Run(
            displayText,
            delay,
            repeat ? onEnd : onEnd + (_ => timersMenu.Remove(timerView)),
            onTick + timerView.Display, repeat);
        _gameViews.Add(timerView);
    }

    public void NewButton(string displayText, UnityAction onClick)
    {
        var view = _UI.Get<ButtonMenuView>().Draw(displayText, onClick);
        _gameViews.Add(view);
    }

    public EnemyView NewEnemy(float delay, Action<EnemyView> onEnd = default, Action<EnemyView> onTick = default)
    {
        var enemy = SinglePool.Instance.Get(_enemyPrefab);
        enemy.transform.position = _enemySpawn.position;
        _enemyViews.Add(enemy);

        _timersService.Run("Enemy steps",
            delay,
            t =>
            {
                onEnd?.Invoke(enemy);
                enemy.Release();
                _enemyViews.Remove(enemy);
            },
            t =>
            {
                onTick?.Invoke(enemy);
                enemy.transform.position = Vector3.Lerp(_enemySpawn.position, _target.position, t.progress);
            });

        return enemy;
    }

    public IEnumerator WaitUntil(Func<bool> evaluatesToTrue)
    {
        while (!evaluatesToTrue.Invoke())
        {
            yield return new WaitWhile(() => Pause);
        }
    }

    public IEnumerator WaitForSeconds(float delay)
    {
        for (float i = 0; i <= delay; i += UnityEngine.Time.deltaTime)
        {
            yield return new WaitWhile(() => Pause);
        }
    }

    public void WarnForSeconds(string message, float duration, Action onEnd = default)
    {
        BlockRaycasts(true);
        StartCoroutine(_UI.Get<GameplayWarnView>().DisplayForSec(message, duration, onEnd));
        BlockRaycasts(false);
    }

    public void WarnWhile(string message, float textDuration, Func<bool> evaluatesToTrue, Action onEnd = default)
    {
        BlockRaycasts(true);
        StartCoroutine(_UI.Get<GameplayWarnView>().DisplayWhile(message, textDuration, evaluatesToTrue, onEnd));
        BlockRaycasts(false);
    }

    public IEnumerator WarnForSecondsRoutine(string message, float duration)
    {
        BlockRaycasts(true);
        yield return _UI.Get<GameplayWarnView>().DisplayForSec(message, duration);
        BlockRaycasts(false);
    }

    public IEnumerator WarnWhileRoutine(string message, float textDuration, Func<bool> evaluatesToTrue)
    {
        BlockRaycasts(true);
        yield return _UI.Get<GameplayWarnView>().DisplayWhile(message, textDuration, evaluatesToTrue);
        BlockRaycasts(false);
    }

    public void BlockRaycasts(bool block)
    {
        _UI.Get<RectPoolable>("RaycastBlocker").gameObject.SetActive(block);
    }
}