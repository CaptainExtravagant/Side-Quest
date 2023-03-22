using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueLane : BaseLaneClass {

    public override void Init(GameManager manager, LaneUI ui)
    {
        baseDodge = 10;
        baseSpeed = 4;
        baseDamage = 6;

        base.Init(manager, ui);
    }

    protected override void Ability()
    {
        gameManager.DamageBoss(currentDamage / 2);

        gameManager.AddGold((int)currentDodge * GetUpgradeLevel());
    }
}
