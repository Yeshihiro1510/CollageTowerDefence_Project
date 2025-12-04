using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Yeshi_Pool;

public class ButtonMenuView : RectPoolable
{
    [SerializeField] private Button _prefab;
    
    private readonly List<Button> _buttons = new List<Button>();

    public Button Draw(string displayText, UnityAction onClick)
    {
        var button = Instantiate(_prefab, transform);
        var ugui = button.GetComponentInChildren<TextMeshProUGUI>();
        if (ugui != null) ugui.text = displayText;
        button.onClick.AddListener(onClick);
        _buttons.Add(button);
        return button;
    }
}