using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardLane : BaseLaneClass {

    public override void Init(GameManager manager)
    {
        baseDamage = 10;
        baseResistance = 7;
        baseSpeed = 6;

        base.Init(manager);
    }

    protected override void Ability()
    {

    }
}
