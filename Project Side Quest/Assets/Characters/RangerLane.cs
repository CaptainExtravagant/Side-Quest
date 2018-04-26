using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerLane : BaseLaneClass {

    public override void Init(GameManager manager)
    {
        baseSpeed = 3;
        baseHealth = 30;
        baseDodge = 6;

        base.Init(manager);
    }

    protected override void Ability()
    {

    }
}
