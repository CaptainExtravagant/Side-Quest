using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueLane : BaseLaneClass {

    public override void Init(GameManager manager)
    {
        baseDodge = 10;
        baseSpeed = 4;
        baseDamage = 6;

        base.Init(manager);
    }

    protected override void Ability()
    {

    }
}
