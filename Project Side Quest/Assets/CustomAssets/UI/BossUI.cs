using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour {

    BaseBossClass currentBoss;

    Text bossName;
    HealthBar healthBar;
    SpecialBar specialTimer;
    Text bossLevel;

    int level;

    public void Init()
    {
        bossLevel = GetComponentsInChildren<Text>()[0];
        bossName = GetComponentsInChildren<Text>()[1];

        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.Init();

        specialTimer = GetComponentInChildren<SpecialBar>();
        specialTimer.Init();
    }

    public void NewBoss(int newLevel, BaseBossClass bossClass)
    {
        currentBoss = bossClass;
        level = newLevel;

        bossLevel.text = level.ToString();

        bossName.text = currentBoss.GetBossName();
        healthBar.NewValues(currentBoss.GetMaxHealth());
        specialTimer.NewBoss(currentBoss.GetSpecialTime());
    }

    public void BossDamaged(float newHealth)
    {
        healthBar.UpdateValues(newHealth);
    }

    public void SpecialUsed()
    {
        specialTimer.ResetTimer();
    }

    public void GamePaused(bool isPaused)
    {
        if(isPaused)
        {
            specialTimer.StopTimer();
        }
        else
        {
            specialTimer.StartTimer();
        }
    }

    public void BossKilled()
    {
        bossName.text = "";
        healthBar.NewValues(0);
        specialTimer.BossKilled();
    }
}
