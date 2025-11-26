using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesMenuView : MonoBehaviour
{
    [SerializeField] private ResourceCounterView _viewPref;
    [SerializeField] private Transform _contentParent;
    private readonly List<ResourceCounterView> _resourceCounterViews = new();

    public ResourceCounterView Draw(string name)
    {
        var newCounter = Instantiate(_viewPref, _contentParent).Init(name);
        _resourceCounterViews.Add(newCounter);
        return newCounter;
    }

    public ResourceCounterView Get(string name) => _resourceCounterViews.FirstOrDefault(v => v.Name == name);
}