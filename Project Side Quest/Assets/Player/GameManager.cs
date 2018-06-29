using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //List of Lanes
    private List<BaseLaneClass> activeLanes = new List<BaseLaneClass>();
    private int laneCount = 0;

    public enum CURRENCY
    {
        INFLUENCE = 0,
        GOLD
    };

    private int influence = 0;
    private int gold = 0;

    int currentLevel = 0;
    BaseBossClass activeBoss;

    float mobSpawnTime = 40;
    float spawnTimer;

    float bossSpawnTime = 5;
    float bossSpawnTimer;
    bool bossSpawned = true;

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

        if (!bossSpawned)
        {
            bossSpawnTimer += Time.deltaTime;
            if (bossSpawnTimer >= bossSpawnTime)
            {
                bossSpawned = true;
                CreateBoss();
            }
        }
    }

    void Init()
    {
        activeLanes.Clear();
        activeBoss = null;

        currentLevel = 0;


        //Spawn Lanes
        activeLanes.Add(Instantiate(Resources.Load("Characters/WarriorLane") as GameObject, new Vector3(0, -0.5f, 0), Quaternion.identity).GetComponent<BaseLaneClass>());
        activeLanes.Add(Instantiate(Resources.Load("Characters/WizardLane") as GameObject, new Vector3(-1.75f, -1.8f, 0), Quaternion.identity).GetComponent<BaseLaneClass>());
        activeLanes.Add(Instantiate(Resources.Load("Characters/RogueLane") as GameObject, new Vector3(1.75f, -1.8f, 0), Quaternion.identity).GetComponent<BaseLaneClass>());
        activeLanes.Add(Instantiate(Resources.Load("Characters/ClericLane") as GameObject, new Vector3(-1.75f, -3.5f, 0), Quaternion.identity).GetComponent<BaseLaneClass>());
        activeLanes.Add(Instantiate(Resources.Load("Characters/RangerLane") as GameObject, new Vector3(1.75f, -3.5f, 0), Quaternion.identity).GetComponent<BaseLaneClass>());

        laneCount = 5;

        uiManager = Instantiate(Resources.Load("UI/InGameUI") as GameObject).GetComponent<UIManager>();
        uiManager.Init(this);

        uiManager.InitLaneUI();

        for (int i = 0; i < activeLanes.Count; i++)
        {
            activeLanes[i].Init(this, uiManager.GetLaneUI(i));
        }

        
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

        bossSpawned = false;

        //Drop Influence
        DropInfluence(currentLevel + 1);
        
        //Drop Gold
        DropGold(currentLevel + 1);

        IncreaseLevel();

        bossSpawnTimer = 0;

        uiManager.NoBoss();
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
            
            uiManager.UpdateGoldCount(gold);                        
        }
    }

    public void EmployLane(int lane)
    {
        if(influence >= (activeLanes[lane].GetCharacterCount() * 200) && activeLanes[lane].IsAlive())
        {
            influence -= activeLanes[lane].GetCharacterCount() * 200;
            activeLanes[lane].Employ();
                        
            uiManager.UpdateInfluenceCount(influence);            
        }
    }

    public void DamageLane(int lane, float damage)
    {
        //Deal Damage
        if (activeLanes[lane].IsAlive())
        {
            activeLanes[lane].Damage(damage);
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
        if (bossSpawned)
        {
            Debug.Log("Character Damage Boss");
            activeBoss.TakeDamage(damage);
        }
    }

    public void UpdateBossHealth()
    {
        uiManager.UpdateBossCurrentHealth(activeBoss.GetCurrentHealth());
    }
}
