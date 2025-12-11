using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Yeshi_Pool;

public class ButtonView : RectPoolable
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;

    public void Init(string text, UnityAction onClick)
    {
        _text.text = text;
        _button.onClick.AddListener(onClick);
    }
}