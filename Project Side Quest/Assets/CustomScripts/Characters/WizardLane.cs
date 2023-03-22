using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardLane : BaseLaneClass {

    public override void Init(GameManager manager, LaneUI ui)
    {
        baseDamage = 10;
        baseResistance = 7;
        baseSpeed = 6;

        base.Init(manager, ui);
    }

    protected override void Ability()
    {
        //Damage Boss
        gameManager.DamageBoss(currentDamage);

        //Damage All Active Mobs
        for (int i = 0; i < 5; i++)
        {
            gameManager.GetLane(i).DamageMobs((int)currentDamage);
        }
    }
}
