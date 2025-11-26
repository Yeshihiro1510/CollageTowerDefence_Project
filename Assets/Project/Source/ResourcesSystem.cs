using System.Collections.Generic;
using System.Linq;

public class ResourcesSystem
{
    private readonly Dictionary<string, Observable<int>> _observables = new();

    public Observable<int> Define(string name, int value)
    {
        _observables[name] = new Observable<int>(value);
        return _observables[name];
    }

    public Observable<int> Get(string name) => _observables[name];
    public (string, Observable<int>)[] GetArray() => _observables.Select(x => (x.Key, x.Value)).ToArray();
    public (string, int)[] Serialize() => _observables.Select(o => (o.Key, o.Value.Value)).ToArray();
}