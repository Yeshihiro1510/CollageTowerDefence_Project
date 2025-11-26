using System;
using System.Collections;
using UnityEngine;

public static class PausedCoroutinesExtension
{
    public static IEnumerator PausedWhile(Func<bool> evaluatesToTrue, Func<bool> pause)
    {
        while (!evaluatesToTrue.Invoke())
            yield return new WaitWhile(pause);
    }
    
    public static IEnumerator PausedUntil(Func<bool> evaluatesToTrue, Func<bool> pause)
    {
        while (!evaluatesToTrue.Invoke())
            yield return new WaitWhile(pause);
    }

    public static IEnumerator PausedSeconds(float seconds, Func<bool> pause)
    {
        float timer = 0f;
        while (timer < seconds)
        {
            timer += Time.deltaTime;
            yield return new WaitWhile(pause);
        }
    }
}