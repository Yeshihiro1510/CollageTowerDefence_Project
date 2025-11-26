using UnityEngine;

public class StartButtonAnimation : MonoBehaviour
{
    public float BaseScale = 1;
    public float Amplitude;
    public float TimeScale;

    private void Awake()
    {
        transform.localScale = Vector2.one * BaseScale;
    }

    private void Update()
    {
        var time = Time.time * TimeScale;
        var sin = Mathf.Sin(time);
        var offset = BaseScale + sin * Amplitude;
        var targetScale = Vector2.one * offset;
        transform.localScale = Vector2.Lerp(transform.localScale, targetScale, Time.deltaTime * TimeScale);
    }
}