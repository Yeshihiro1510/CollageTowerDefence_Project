using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yeshi_Pool;

public class ButtonMenuView : RectPoolable
{
    [SerializeField] private ButtonView _prefab;
    
    private readonly List<ButtonView> _buttons = new List<ButtonView>();

    public ButtonView Draw(string displayText, UnityAction onClick)
    {
        var button = SinglePool.Instance.Get(_prefab);
        button.Init(displayText, onClick);
        button.transform.SetParent(transform);
        _buttons.Add(button);
        return button;
    }
}