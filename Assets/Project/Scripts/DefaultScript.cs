using System.Collections;

public class DefaultScript : GameplayScript
{
    public override IEnumerator Script()
    {
        var Wheat = Main.NewResource("Wheat", 10);
        var Villagers = Main.NewResource("Villagers", 3);
        var Soldiers = Main.NewResource("Soldiers", 1);

        Main.NewTimer("Wheat incomes", 3, _ => Wheat.Value += Villagers.Value * 1, repeat: true);
        Main.NewTimer("Soldiers support payment", 5, _ => Wheat.Value -= Soldiers.Value * 2, repeat: true);

        Main.NewButton("Hire villagers",
            () => Main.NewTimer("Villagers hiring", 7, _ => Villagers.Value++));
        Main.NewButton("Hire soldiers", 
            () => Main.NewTimer("Soldiers hiring", 15, _ => Soldiers.Value++));

        yield break;
    }
}