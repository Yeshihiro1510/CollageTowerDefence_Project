using UnityEngine;
using UnityEngine.SceneManagement;

public class DevTools : MonoBehaviour
{
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
#endif
}