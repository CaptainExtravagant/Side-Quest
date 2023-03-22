using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerLane : BaseLaneClass {

    public override void Init(GameManager manager, LaneUI ui)
    {
        baseSpeed = 3;
        baseHealth = 30;
        baseDodge = 6;

        base.Init(manager, ui);
    }

    protected override void Ability()
    {
        gameManager.DamageBoss(currentDamage / 2);

        gameManager.AddInfluence((int)currentDodge * GetUpgradeLevel());
    }
}
