using TMPro;
using UnityEngine;
using Yeshi_Pool;

public class CounterView : RectPoolable
{
    private const string FORMAT = "{0}: {1}";
    
    [SerializeField] private TMP_Text _text;
    
    private string _displayText;

    public void Init(string displayText)
    {
        _displayText = displayText;
    }

    public void Display(int value) => _text.text = string.Format(FORMAT, _displayText, value);
}