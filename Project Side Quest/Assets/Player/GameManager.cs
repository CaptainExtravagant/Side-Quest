using System.Collections;
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
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void BossDefeated()
    {
        Destroy(activeBoss.gameObject);
        activeBoss = null;

        //Drop Influence
        DropInfluence(50 * (currentLevel + 1));
        
        //Drop Gold
        DropGold(20 * (currentLevel + 1));

        IncreaseLevel();

        CreateBoss();
    }

    void DropInfluence(int amountToDrop)
    {
        influence += amountToDrop;
    }

    public void DropGold(float amountToDrop)
    {
        //Drop Gold Objects
        gold += (int)amountToDrop;
    }

    void IncreaseLevel()
    {
        Debug.Log("Increasing Level");
        currentLevel++;

        for (int i = 0; i < activeLanes.Count; i++)
        {
            activeLanes[i].LevelUp();
        }

        laneCount = activeLanes.Count;
    }

    public void UpgradeLane(int lane)
    {
        if (gold >= (activeLanes[lane].GetUpgradeLevel() * 100))
        {
            gold -= activeLanes[lane].GetUpgradeLevel() * 100;
            activeLanes[lane].Upgrade();
        }
    }

    public void EmployLane(int lane)
    {
        if(influence >= (activeLanes[lane].GetCharacterCount() * 200))
        {
            influence -= activeLanes[lane].GetCharacterCount() * 200;
            activeLanes[lane].Employ();
        }
    }

    public void DamageLane(int lane, float damage)
    {
        if(activeLanes[lane])
            activeLanes[lane].Damage(damage);
    }

    public void AddMobs(int lane, int mobCount)
    {
        if (activeLanes[lane])
            activeLanes[lane].AddMobs(mobCount);
    }

    public void LaneDestroyed()
    {
        laneCount--;
        if(laneCount <= 0)
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
        }

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
}
