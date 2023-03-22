using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericLane : BaseLaneClass {

    public override void Init(GameManager manager, LaneUI ui)
    {
        baseResistance = 10;
        baseHealth = 15;
        baseDamage = 3;

        base.Init(manager, ui);
    }

    protected override void Ability()
    {
        for(int i = 0; i < 5; i++)
        {
            gameManager.GetLane(i).Heal(healthModifier * GetUpgradeLevel());
        }
    }
}
