using System.Collections;

public class DefaultScript : GameplayScript
{
    private Observable<int> wheat;
    private Observable<int> villagers;
    private Observable<int> soldiers;
    
    public override void Assemble()
    {
        Resource("Wheat", 10, out wheat);
        Resource("Villagers", 3, out villagers);
        Resource("Soldiers", 1, out soldiers);
        DrawButton("Hire villager").OnClick(VillagerHire);
        DrawButton("Hire soldier").OnClick(SoldierHire);
    }

    public override IEnumerator Script()
    {
        Timers.Start("Wheat incomes", 3, () =>
        {
            wheat.Value += villagers.Value * 1;
        }, true);
        Timers.Start("Soldiers support cost", 5, () =>
        {
            wheat.Value -= soldiers.Value * 2;
        }, true);
        
        

        yield return null;
    }

    private void VillagerHire()
    {
        Timers.Start("Villager hire", 5, () => villagers.Value++);
    }

    private void SoldierHire()
    {
        Timers.Start("Soldier hire", 10, () => soldiers.Value++);
    }
}