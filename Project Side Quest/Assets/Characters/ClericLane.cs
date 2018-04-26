using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericLane : BaseLaneClass {

    public override void Init(GameManager manager)
    {
        baseResistance = 10;
        baseHealth = 15;
        baseDamage = 3;

        base.Init(manager);
    }

    protected override void Ability()
    {

    }
}
