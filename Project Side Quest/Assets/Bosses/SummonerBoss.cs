using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerBoss : BaseBossClass {
    
    public override void Init(GameManager manager)
    {
        bossName = "Necro Summoner";

        baseHealth = 80;
        specialSpeed = 15;

        damageEffect = 1;

        base.Init(manager);
    }

    protected override void SpecialAttack()
    {
        base.SpecialAttack();
        gameManager.AddMobs(targetLane, (int)baseHealth * ((int)healthModifier / 2));
    }
}
