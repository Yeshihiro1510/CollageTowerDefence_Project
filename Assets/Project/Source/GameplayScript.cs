using System;
using System.Collections;

public abstract class GameplayScript
{
    public UI UI { get; set; }
    public TimersSystem Timers { get; set; }
    public ResourcesSystem Resources { get; set; }
    public Func<bool> IsPaused { get; set; }

    public abstract void Assemble();
    public abstract IEnumerator Script();

    public void Resource(string resourceName, int value, out Observable<int> observable, string displayText = "")
    {
        if (displayText == "") displayText = resourceName;

        var view = UI.ResourcesMenuView.Draw(displayText);
        observable = Resources.Define(resourceName, value);
        observable.Subscribe(view.Display);
        view.Display(value);
    }

    public ButtonView DrawButton(string text) => UI.ButtonMenuView.Draw(text);
}

// public struct Services
// {
//     public Services(UI UI, TimersSystem timers, ResourcesSystem resourcesSystem, Func<bool> isPaused)
//     {
//         this.UI = UI;
//         this.timers = timers;
//         this.resourcesSystem = resourcesSystem;
//         this.isPaused = isPaused;
//     }
//
//     public readonly UI UI;
//     public readonly TimersSystem timers;
//     public readonly ResourcesSystem resourcesSystem;
//     public readonly Func<bool> isPaused;
// }