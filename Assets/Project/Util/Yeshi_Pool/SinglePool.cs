using UnityEngine.SceneManagement;
using Yeshi_Pool;

public class SinglePool
{
    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ObjectPool();
                SceneManager.sceneUnloaded += OnSceneUnloaded;
            }

            return _instance;
        }
    }
    private static ObjectPool _instance;

    private static void OnSceneUnloaded(Scene _)
    {
        _instance.Clear();
    }
}