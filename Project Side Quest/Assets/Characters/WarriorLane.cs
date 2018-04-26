using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorLane : BaseLaneClass {

    public override void Init(GameManager manager)
    {
        baseHealth = 40;
        baseDamage = 6;
        baseSpeed = 7;

        base.Init(manager);
    }

    protected override void Ability()
    {
        
    }
}
