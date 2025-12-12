using System.Linq;
using UnityEngine;
using Yeshi_Pool;

public class UI : MonoBehaviour
{
    private RectPoolable[] _elements;
    private RectPoolable _cache;

    public void Init()
    {
        _elements = GetComponentsInChildren<RectPoolable>(true);
    }

    public void SetState(params string[] names)
    {
        foreach (var e in _elements)
        {
            if (names.Contains(e.name)) e.TurnOn();
            else e.TurnOff();
        }
    }

    public T Get<T>() where T : RectPoolable, new()
    {
        if (_cache is T result) return result;
        var obj = _elements.First(e => e is T) as T;
        _cache = obj;
        return obj;
    }

    public T Get<T>(string elementName) where T : RectPoolable, new()
    {
        if (_cache?.name == elementName && _cache is T result) return result;
        var obj = _elements.First(e => e is T && e.name == elementName) as T;
        _cache = obj;
        return obj;
    }
}