using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonBoss : BaseBossClass {

    public override void Init(GameManager manager)
    {
        bossName = "Vile Gorgon";

        attackSpeed = 3;

        damageEffect = 3;
        
        base.Init(manager);
    }

    protected override void SpecialAttack()
    {
        base.SpecialAttack();
        gameManager.LaneEffect(targetLane, damageEffect, currentSpecial);
    }
}
