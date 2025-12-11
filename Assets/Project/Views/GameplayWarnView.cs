using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yeshi_Pool;

public class GameplayWarnView : RectPoolable
{
    [SerializeField] private TMP_Text _gameLabel;
    [SerializeField] private Image _background;
    [SerializeField] private float _textSpeed;
    [SerializeField] private float _backgroundSpeed = 5f;
    [SerializeField, Range(0f, 1f)] private float _saturation = 1;
    [SerializeField, Range(0f, 1f)] private float _value = 1;
    private float _timer;
    
    private void Update()
    {
        _timer += Time.deltaTime * _backgroundSpeed;
        _background.color = Color.HSVToRGB((_timer % 360).normalize(360), _saturation, _value);
    }

    public IEnumerator DisplayForSec(string message, float duration, Action callback = default)
    {
        TurnOn();
        _gameLabel.text = message;
        var boxedTime = duration / 3;
        var textDuration = boxedTime / _textSpeed;
        var charDuration = textDuration / message.Length;

        for (int i = 0; i <= message.Length; i++)
        {
            yield return new WaitForSeconds(charDuration);
            _gameLabel.maxVisibleCharacters = i;
        }

        yield return new WaitForSeconds(boxedTime);

        for (int i = message.Length; i >= 0; i--)
        {
            yield return new WaitForSeconds(charDuration);
            _gameLabel.maxVisibleCharacters = i;
        }

        _gameLabel.text = string.Empty;
        TurnOff();
        callback?.Invoke();
    }

    public IEnumerator DisplayWhile(string message, float textDuration, Func<bool> evaluatesToTrue, Action callback = default)
    {
        _gameLabel.text = message;
        _gameLabel.maxVisibleCharacters = 0;
        var charDuration = textDuration / 2 / message.Length;
        TurnOn();

        for (int i = 0; i <= message.Length; i++)
        {
            yield return new WaitForSeconds(charDuration);
            _gameLabel.maxVisibleCharacters = i;
        }

        yield return new WaitUntil(evaluatesToTrue);

        for (int i = message.Length; i >= 0; i--)
        {
            yield return new WaitForSeconds(charDuration);
            _gameLabel.maxVisibleCharacters = i;
        }

        _gameLabel.text = string.Empty;
        TurnOff();
        callback?.Invoke();
    }
}