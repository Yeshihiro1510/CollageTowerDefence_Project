using System;

public class ObservableProperty<T>
{
    public ObservableProperty(T value, Func<T> getter = null, Func<T, T> setter = null)
    {
        _value = value;
        _getter = getter;
        _setter = setter;
    }

    public ObservableProperty(ref T reference, Func<T> getter = null, Func<T, T> setter = null)
    {
        _value = reference;
        _getter = getter;
        _setter = setter;
    }

    public T Value
    {
        get => _getter == null ? _value : _getter();
        set
        {
            if (_value.Equals(value)) return;
            _value = _setter == null ? value : _setter(value);
            _onChanged?.Invoke(_value);
        }
    }

    private event Action<T> _onChanged;
    private readonly Func<T> _getter;
    private readonly Func<T, T> _setter;
    private T _value;

    public void Bind(Action<T> subscriber)
    {
        _onChanged += subscriber;
        subscriber?.Invoke(Value);
    }

    public void Unbind(Action<T> subscriber)
    {
        _onChanged -= subscriber;
    }
}