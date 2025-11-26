using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonMenuView : MonoBehaviour
{
    [SerializeField] private ButtonView _pref;
    [SerializeField] private Transform _parent;
    private readonly List<ButtonView> _buttons = new();

    public ButtonView Draw(string text)
    {
        var newButton = Instantiate(_pref, _parent).Init(text);
        _buttons.Add(newButton);
        return newButton;
    }
    
    public ButtonView Get(string text) => _buttons.First(b => b.Text == text);
}