using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;

    public string Text
    {
        get => _text.text;
        set => _text.text = value;
    }

    public ButtonView Init(string text)
    {
        Text = text;
        return this;
    }

    public void OnClick(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void Unsubscribe(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }
}