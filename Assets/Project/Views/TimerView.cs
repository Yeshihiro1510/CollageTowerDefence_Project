using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yeshi_Pool;

public class TimerView : RectPoolable
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    private bool _reverse;

    public void Init(string displayText, bool reverse = false)
    {
        _text.text = displayText;
        _reverse = reverse;
    }

    public void Display(TimerData timer)
    {
        _image.fillAmount = (timer.currentTime - timer.startTime).Normalize(timer.delay, _reverse);
    }
}
