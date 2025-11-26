using UnityEngine;

public static class Initializer
{
    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        DevTools devTools = new GameObject("[Dev Tools]").AddComponent<DevTools>();
        Object.DontDestroyOnLoad(devTools);
    }
}