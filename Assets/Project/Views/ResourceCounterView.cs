using System;
using TMPro;
using UnityEngine;

public class ResourceCounterView : MonoBehaviour
{
    private const string TextFormat = "{0}: {1}";
    
    [SerializeField] private TMP_Text _text;

    public string Name { get; private set; }

    public ResourceCounterView Init(string name)
    {
        Name = name;
        gameObject.name = Name + "CounterView";
        return this;
    }

    public void ObserveValue(ref Action<int> onChange)
    {
        onChange += Display;
    }

    public void UnObserveValue(ref Action<int> onChange)
    {
        onChange -= Display;
    }

    public void Display(int value)
    {
        _text.text = string.Format(TextFormat, Name, value);
    }
}