using System;
using System.Collections;
using UnityEngine;

public class DefaultScript : GameplayScript
{
    public override IEnumerator Script()
    {
        int startWheat = 10;
        int startVillagers = 3;
        int startSoldiers = 1;
        float villagerHireTime = 7f;
        float soldierHireTime = 10f;
        float wheatIncomeTime = 3f;
        float soldierSupportPaymentTime = 5f;
        int wheatPerVillagerIncome = 1;
        int supportPaymentPerSoldier = 2;
        int wavesCount = 10;
        Func<int, int> enemiesCountPerWave = i => i;
        Func<int, float> timePerEnemies = i => 3;
        Func<int, float> enemyComeDuration = i => 10;

        // Setup game

        var Wheat = Game.NewResource("Wheat", startWheat);
        var Villagers = Game.NewResource("Villagers", startVillagers);
        var Soldiers = Game.NewResource("Soldiers", startSoldiers);

        Game.NewButton(
            "Hire villagers",
            () => Game.NewTimer("Villagers hiring",
                villagerHireTime,
                _ => Villagers.Value++,
                reverse: true));
        Game.NewButton(
            "Hire soldiers",
            () => Game.NewTimer("Soldiers hiring",
                soldierHireTime,
                _ => Soldiers.Value++,
                reverse: true));

        Game.NewTimer("Wheat incomes",
            wheatIncomeTime,
            _ => Wheat.Value += Villagers.Value * wheatPerVillagerIncome,
            repeat: true,
            reverse: true);
        Game.NewTimer("Soldiers support payment",
            soldierSupportPaymentTime,
            _ => Wheat.Value -= Soldiers.Value * supportPaymentPerSoldier,
            repeat: true,
            reverse: true);

        // Gameplay

        var fail = false;
        for (var i = 1; i <= wavesCount && !fail; i++)
        {
            Game.Pause = true;
            yield return Game.WarnForSecondsRoutine($"Stage {i}", Timings.STAGES / Game.TimeScale);
            Game.Pause = false;

            for (var j = 0; j < enemiesCountPerWave.Invoke(i); j++)
            {
                Game.NewEnemy(enemyComeDuration.Invoke(i), _ =>
                {
                    if (Soldiers.Value > 0) Soldiers.Value--;
                    else if (Villagers.Value > 0) Villagers.Value -= 3;
                    fail = Villagers.Value == 0;
                });
                yield return Game.WaitForSeconds(timePerEnemies.Invoke(i));
            }

            yield return Game.WaitUntil(() => Game.Enemies == 0);
        }

        if (fail)
            yield return Game.WarnWhileRoutine("Its fail!", Timings.WIN_FAIL / Game.TimeScale,
                () => Input.GetMouseButtonDown(0));
        else
            yield return Game.WarnWhileRoutine("You win!", Timings.WIN_FAIL / Game.TimeScale,
                () => Input.GetMouseButtonDown(0));
    }
}