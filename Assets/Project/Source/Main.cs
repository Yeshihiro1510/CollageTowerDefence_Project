using System.Collections;
using UnityEngine;

public static class Scenes
{
    public const string MAIN = "Main";
}

public class Main : MonoBehaviour
{
    [SerializeField] private UI _UI;

    private TimersSystem _timersService;
    private ResourcesSystem _resourcesService;

    private bool _isPaused;
    private bool _isGameEnded;

    private void Awake()
    {
        _resourcesService = new ResourcesSystem();
        _timersService = new TimersSystem();
    }

    private void Start()
    {
        GameplayScript script = new DefaultScript()
        {
            UI = _UI,
            Timers = _timersService,
            Resources = _resourcesService,
            IsPaused = () => _isPaused
        };
        StartCoroutine(MainRoutine(script));
    }

    private void OnEnable() => _isPaused = false;
    private void OnDisable() => _isPaused = true;

    private void Update()
    {
        if (_isPaused) return;

        _timersService.Tick();
    }

    private IEnumerator MainRoutine(GameplayScript script)
    {
        script.Assemble();

        _UI.SetStartScreenScreen();
        yield return PausedCoroutinesExtension.PausedWhile(() => Input.anyKeyDown, () => _isPaused);
        _UI.SetGameplayScreen();
        yield return script.Script();
        yield return PausedCoroutinesExtension.PausedUntil(() => _isGameEnded, () => _isPaused);
        _timersService.StopAll();
    }
}