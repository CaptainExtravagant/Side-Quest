﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //List of Lanes
    private List<BaseLaneClass> activeLanes = new List<BaseLaneClass>();
    private int laneCount = 0;

    private int influence = 0;
    private int gold = 0;

    int currentLevel = 0;
    BaseBossClass activeBoss;

    float mobSpawnTime = 40;
    float spawnTimer;

    UIManager uiManager;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= mobSpawnTime)
        {
            activeLanes[Random.Range(0, 4)].AddMobs(10 * (currentLevel + 1));
        }
    }

    void Init()
    {
        activeLanes.Clear();
        activeBoss = null;

        currentLevel = 0;

        //Spawn Lanes
        activeLanes.Add(Instantiate(Resources.Load("Characters/WarriorLane") as GameObject, new Vector3(0, -0.5f, 0), Quaternion.identity).GetComponent<BaseLaneClass>());
        activeLanes.Add(Instantiate(Resources.Load("Characters/WizardLane") as GameObject, new Vector3(-1.5f, -1.8f, 0), Quaternion.identity).GetComponent<BaseLaneClass>());
        activeLanes.Add(Instantiate(Resources.Load("Characters/RogueLane") as GameObject, new Vector3(1.5f, -1.8f, 0), Quaternion.identity).GetComponent<BaseLaneClass>());
        activeLanes.Add(Instantiate(Resources.Load("Characters/ClericLane") as GameObject, new Vector3(-1.5f, -3.5f, 0), Quaternion.identity).GetComponent<BaseLaneClass>());
        activeLanes.Add(Instantiate(Resources.Load("Characters/RangerLane") as GameObject, new Vector3(1.5f, -3.5f, 0), Quaternion.identity).GetComponent<BaseLaneClass>());

        laneCount = 5;

        for (int i = 0; i < activeLanes.Count; i++)
        {
            activeLanes[i].Init(this);
        }

        uiManager = Instantiate(Resources.Load("UI/InGameUI") as GameObject).GetComponent<UIManager>();
        uiManager.Init(this);

        CreateBoss();
    }

    void CreateBoss()
    {
        GameObject newBoss;

        //Create new boss
        switch(Random.Range(0, 4))
        {
            case 0:
                newBoss = Instantiate(Resources.Load("Bosses/ZombieBoss") as GameObject, new Vector3(0, 3, 0), Quaternion.identity);
                activeBoss = newBoss.GetComponent<BaseBossClass>();
                break;

            case 1:
                newBoss = Instantiate(Resources.Load("Bosses/GorgonBoss") as GameObject, new Vector3(0, 3, 0), Quaternion.identity);
                activeBoss = newBoss.GetComponent<BaseBossClass>();
                break;

            case 2:
                newBoss = Instantiate(Resources.Load("Bosses/SummonerBoss") as GameObject, new Vector3(0, 3, 0), Quaternion.identity);
                activeBoss = newBoss.GetComponent<BaseBossClass>();
                break;

            case 3:
                newBoss = Instantiate(Resources.Load("Bosses/CursedBoss") as GameObject, new Vector3(0, 3, 0), Quaternion.identity);
                activeBoss = newBoss.GetComponent<BaseBossClass>();
                break;

            default:
                newBoss = Instantiate(Resources.Load("Bosses/ZombieBoss") as GameObject, new Vector3(0,3,0), Quaternion.identity);
                activeBoss = newBoss.GetComponent<BaseBossClass>();                
                break;
        }

        activeBoss.Init(this);

        uiManager.NewBoss((int)activeBoss.GetMaxHealth());
    }

    public UIManager GetUIManager()
    {
        return uiManager;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public BaseBossClass GetCurrentBoss()
    {
        return activeBoss;
    }

    public void BossDefeated()
    {
        Destroy(activeBoss.gameObject);
        activeBoss = null;

        //Drop Influence
        DropInfluence(10 * (currentLevel + 1));
        
        //Drop Gold
        DropGold(10 * (currentLevel + 1));

        IncreaseLevel();

        CreateBoss();
    }

    void DropInfluence(int amountToDrop)
    {
		//Drop Influence Objects
		for (int i = 0; i < amountToDrop; i++) {
			Instantiate (Resources.Load ("Pickups/InfluencePickup") as GameObject, new Vector3 (Random.Range(-1.0f, 1.0f), Random.Range(0.5f, 2.5f), 0), Quaternion.identity).GetComponent<InfluencePickups>().Init(currentLevel, this);
		}
    }

    public void DropGold(float amountToDrop)
    {
        //Drop Gold Objects
		for (int i = 0; i < amountToDrop; i++) {
			Instantiate (Resources.Load ("Pickups/GoldPickup") as GameObject, new Vector3 (Random.Range(-1.0f, 1.0f), Random.Range(0.5f, 2.5f), 0), Quaternion.identity).GetComponent<GoldPickups>().Init(currentLevel, this);
		}
    }

    public void AddGold(int add)
    {
        gold += add;
        uiManager.UpdateGoldCount(gold);
    }

    public void AddInfluence(int add)
    {
        influence += add;
        uiManager.UpdateInfluenceCount(influence);
    }

    void IncreaseLevel()
    {
        Debug.Log("Increasing Level");
        currentLevel++;

        for (int i = 0; i < activeLanes.Count; i++)
        {
            if(!activeLanes[i].IsAlive())
                uiManager.ResetLane(i);

            activeLanes[i].LevelUp();
            uiManager.UpdateLaneHealth(i, activeLanes[i].GetSingleHealth());
        }

        laneCount = activeLanes.Count;
    }

    public BaseLaneClass GetLane(int lane)
    {
        return activeLanes[lane];
    }

    public void UpgradeLane(int lane)
    {
        if (gold >= (activeLanes[lane].GetUpgradeLevel() * 100) && activeLanes[lane].IsAlive())
        {
            gold -= activeLanes[lane].GetUpgradeLevel() * 100;
            activeLanes[lane].Upgrade();

            uiManager.UpdateLaneValue(lane, 1, activeLanes[lane].GetUpgradeLevel() * 100);
            uiManager.UpdateGoldCount(gold);

            uiManager.LaneUpgraded(lane, activeLanes[lane].GetUpgradeLevel(), activeLanes[lane].GetSingleHealth());
        }
    }

    public void EmployLane(int lane)
    {
        if(influence >= (activeLanes[lane].GetCharacterCount() * 200) && activeLanes[lane].IsAlive())
        {
            influence -= activeLanes[lane].GetCharacterCount() * 200;
            activeLanes[lane].Employ();

            uiManager.UpdateLaneValue(lane, 0, activeLanes[lane].GetCharacterCount() * 200);
            uiManager.UpdateInfluenceCount(influence);

            uiManager.LaneEmployed(lane);
        }
    }

    public void DamageLane(int lane, float damage)
    {
        //Deal Damage
        if (activeLanes[lane].IsAlive())
        {
            if (activeLanes[lane].Damage(damage))
            {
                if (activeLanes[lane].GetSingleHealth() <= 0)
                {
                    //Check Current Health
                    uiManager.LaneCharacterLost(lane, activeLanes[lane].CheckHealth());
                }

                uiManager.UpdateLaneHealth(lane, activeLanes[lane].GetSingleHealth());

                LaneDestroyed(lane);
            }
        }
        
    }

    public void AddMobs(int lane, int mobCount)
    {
        if (activeLanes[lane])
            activeLanes[lane].AddMobs(mobCount);
    }

    public void LaneDestroyed(int lane)
    {
        laneCount--;
        if(activeLanes[0].IsAlive() == false &&
            activeLanes[1].IsAlive() == false &&
            activeLanes[2].IsAlive() == false &&
            activeLanes[3].IsAlive() == false &&
            activeLanes[4].IsAlive() == false)
        {
            //Game Over
            ResetGame();
        }
    }

    private void ResetGame()
    {
        Debug.Log("Game Lost, Resetting");

        Destroy(activeBoss.gameObject);
        activeBoss = null;

        laneCount = activeLanes.Count;

        for (int i = 0; i < activeLanes.Count; i++)
        {
            activeLanes[i].ResetStats();
            uiManager.ResetLane(i);
        }

        gold /= 2;
        influence /= 2;

        uiManager.LevelReset(gold, influence);

        currentLevel = 0;
        CreateBoss();
    }

    public void LaneEffect(int lane, int effect, float time)
    {
        activeLanes[lane].EnableEffects(effect, time);
    }

    public void DamageBoss(float damage)
    {
        Debug.Log("Character Damage Boss");
        activeBoss.TakeDamage(damage);
    }

    public void UpdateBossHealth()
    {
        uiManager.UpdateBossCurrentHealth(activeBoss.GetCurrentHealth());
    }
}
