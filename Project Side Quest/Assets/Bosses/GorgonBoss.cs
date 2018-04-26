using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonBoss : BaseBossClass {

    public override void Init(GameManager manager)
    {
        attackSpeed = 3;

        damageEffect = 3;
        
        base.Init(manager);
    }

    protected override void SpecialAttack()
    {
        base.SpecialAttack();
    }
}
