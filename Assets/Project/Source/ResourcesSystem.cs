using System.Collections.Generic;
using System.Linq;

public class ResourcesSystem
{
    private readonly Dictionary<string, ObservableProperty<int>> _observables = new();

    public ObservableProperty<int> Define(string name, int value)
    {
        _observables[name] = new ObservableProperty<int>(value);
        return _observables[name];
    }

    public ObservableProperty<int> Get(string name) => _observables[name];
    public (string, ObservableProperty<int>)[] GetArray() => _observables.Select(x => (x.Key, x.Value)).ToArray();
    public (string, int)[] Serialize() => _observables.Select(o => (o.Key, o.Value.Value)).ToArray();
    public void Clear() => _observables.Clear();
}