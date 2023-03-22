using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBoss : BaseBossClass {

    public override void Init(GameManager manager)
    {
        bossName = "Zombie King";

        baseHealth = 150;

        base.Init(manager);
    }

    protected override void SpecialAttack()
    {
        base.SpecialAttack();
        Heal(Random.Range(40, 80) * healthModifier);
    }
}
