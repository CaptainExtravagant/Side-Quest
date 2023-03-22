using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedBoss : BaseBossClass {

    public override void Init(GameManager manager)
    {
        bossName = "Cursed Warrior";
        
        baseAttack = 15;

        damageEffect = 2;
        
        base.Init(manager);
    }

    protected override void SpecialAttack()
    {
        base.SpecialAttack();
        gameManager.DamageLane(targetLane, baseAttack * 2 * attackModifier);
    }
}
