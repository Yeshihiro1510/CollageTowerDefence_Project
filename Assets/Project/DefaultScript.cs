using System.Collections;
using UnityEngine;

public class DefaultScript : GameplayScript
{
    public override IEnumerator Script()
    {
        var Wheat = Game.NewResource("Wheat", 10);
        var Villagers = Game.NewResource("Villagers", 3);
        var Soldiers = Game.NewResource("Soldiers", 1);

        Game.NewButton(
            "Hire villagers",
            () => Game.NewTimer("Villagers hiring",
                7,
                _ => Villagers.Value++,
                reverse: true));
        Game.NewButton(
            "Hire soldiers",
            () => Game.NewTimer("Soldiers hiring",
                15,
                _ => Soldiers.Value++,
                reverse: true));

        Game.NewTimer("Wheat incomes",
            3,
            _ => Wheat.Value += Villagers.Value * 1,
            repeat: true,
            reverse: true);
        Game.NewTimer("Soldiers support payment",
            5,
            _ => Wheat.Value -= Soldiers.Value * 2,
            repeat: true,
            reverse: true);

        for (int i = 1; i < 10; i++)
        {
            yield return Game.WarnForSecondsRoutine($"Stage {i}", 3);
            for (int j = 0; j < i * 2; j++)
            {
                Game.NewEnemy(10f, _ =>
                {
                    if (Soldiers.Value > 0) Soldiers.Value--;
                    else if (Villagers.Value > 0) Villagers.Value -= 3;

                    if (Villagers.Value <= 0) Fail();
                });
                yield return Game.WaitForSeconds(3);
            }

            yield return Game.WaitUntil(() => Game.Enemies == 0);
        }

        Win();
    }

    public void Win()
    {
        Game.StopGame();
        Game.WarnWhile("Win!", 1, () => Input.GetKeyDown(KeyCode.Escape), Game.LoadMenu);
    }

    public void Fail()
    {
        Game.StopGame();
        Game.WarnWhile("Fail!", 1, () => Input.GetKeyDown(KeyCode.Escape), Game.LoadMenu);
    }
}