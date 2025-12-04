using System.Collections.Generic;
using UnityEngine;
using Yeshi_Pool;

public class CounterMenuView : RectPoolable
{
    [SerializeField] private CounterView _prefab;
    
    private readonly List<CounterView> _textViews = new List<CounterView>();

    public CounterView Draw(string displayText)
    {
        var text = SinglePool.Instance.Get(_prefab);
        text.transform.SetParent(transform);
        text.Init(displayText);
        _textViews.Add(text);
        return text;
    }

    public void Remove(CounterView textView)
    {
        _textViews.Remove(textView);
        textView.ResetObject();
    }
}