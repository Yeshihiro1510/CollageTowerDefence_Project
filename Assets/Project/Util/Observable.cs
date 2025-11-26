using System;

public class Observable<T>
{
    public Observable(T value)
    {
        Value = value;
    }

    public Observable(ref T obj)
    {
        _value = obj;
    }

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            _onChanged?.Invoke(_value);
        }
    }

    private Action<T> _onChanged;
    private T _value;

    public void Subscribe(Action<T> subscriber)
    {
        _onChanged += subscriber;
    }

    public void Unsubscribe(Action<T> subscriber)
    {
        _onChanged -= subscriber;
    }
}