using UnityEngine;

public class JumpingScaleEffect : MonoBehaviour
{
    public float BaseSize = 1;
    public float Amplitude = 0.3f;
    public float TimeScale = 2;

    private void Start()
    {
        transform.localScale = Vector2.one * BaseSize;
    }

    private void Update()
    {
        var sin = Mathf.Sin(Time.time * TimeScale);
        var targetScale = Vector2.one * (BaseSize + sin * (Amplitude * BaseSize));
        transform.localScale = Vector2.Lerp(transform.localScale, targetScale, Time.deltaTime * TimeScale);
    }
}