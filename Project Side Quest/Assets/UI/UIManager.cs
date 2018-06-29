using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private GameManager manager;

    private Text influenceText;
    private Text goldText;

    private HealthBar bossHealthBar;

    private float bossCurrentHealth;
    private float bossMaxHealth;

    private int round = 0;
    private Text roundText;

    private LaneUI warriorLane;
    private LaneUI wizardLane;
    private LaneUI rogueLane;
    private LaneUI clericLane;
    private LaneUI rangerLane;

    public void Init(GameManager gameManager)
    {
        manager = gameManager;

        //Get Currency Texts
        influenceText = GetComponentsInChildren<Text>()[0];
        goldText = GetComponentsInChildren<Text>()[1];

        influenceText.text = "0";
        goldText.text = "0";

        //Get Boss Health Bar
        bossHealthBar = GetComponentInChildren<HealthBar>();
        bossHealthBar.Init();

        //Get Round Text
        roundText = GetComponentsInChildren<Text>()[5];

        //Get Lanes
        warriorLane = GetComponentsInChildren<LaneUI>()[0];        
        wizardLane = GetComponentsInChildren<LaneUI>()[1];
        rogueLane = GetComponentsInChildren<LaneUI>()[2];
        clericLane = GetComponentsInChildren<LaneUI>()[3];
        rangerLane = GetComponentsInChildren<LaneUI>()[4];        
    }

    public void InitLaneUI()
    {
        //Init Lane Health Bars
        warriorLane.Init(manager.GetLane(0).GetSingleHealth());
        wizardLane.Init(manager.GetLane(1).GetSingleHealth());
        rogueLane.Init(manager.GetLane(2).GetSingleHealth());
        clericLane.Init(manager.GetLane(3).GetSingleHealth());
        rangerLane.Init(manager.GetLane(4).GetSingleHealth());

        //Set Lane Upgrade Values
        warriorLane.GetComponentsInChildren<Text>()[1].text = (manager.GetLane(0).GetCharacterCount() * 200).ToString();
        warriorLane.GetComponentsInChildren<Text>()[2].text = (manager.GetLane(0).GetUpgradeLevel() * 100).ToString();

        wizardLane.GetComponentsInChildren<Text>()[1].text = (manager.GetLane(1).GetCharacterCount() * 200).ToString();
        wizardLane.GetComponentsInChildren<Text>()[2].text = (manager.GetLane(1).GetUpgradeLevel() * 100).ToString();

        rogueLane.GetComponentsInChildren<Text>()[1].text = (manager.GetLane(2).GetCharacterCount() * 200).ToString();
        rogueLane.GetComponentsInChildren<Text>()[2].text = (manager.GetLane(2).GetUpgradeLevel() * 100).ToString();

        clericLane.GetComponentsInChildren<Text>()[1].text = (manager.GetLane(3).GetCharacterCount() * 200).ToString();
        clericLane.GetComponentsInChildren<Text>()[2].text = (manager.GetLane(3).GetUpgradeLevel() * 100).ToString();

        rangerLane.GetComponentsInChildren<Text>()[1].text = (manager.GetLane(4).GetCharacterCount() * 200).ToString();
        rangerLane.GetComponentsInChildren<Text>()[2].text = (manager.GetLane(4).GetUpgradeLevel() * 100).ToString();

        //Bind Lane Upgrade Buttons
        warriorLane.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate { manager.EmployLane(0); });
        warriorLane.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate { manager.UpgradeLane(0); });


        wizardLane.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate { manager.EmployLane(1); });
        wizardLane.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate { manager.UpgradeLane(1); });


        rogueLane.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate { manager.EmployLane(2); });
        rogueLane.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate { manager.UpgradeLane(2); });


        clericLane.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate { manager.EmployLane(3); });
        clericLane.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate { manager.UpgradeLane(3); });


        rangerLane.GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate { manager.EmployLane(4); });
        rangerLane.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate { manager.UpgradeLane(4); });

    }

    public LaneUI GetLaneUI(int lane)
    {
        switch(lane)
        {
            case 0:
                return warriorLane;

            case 1:
                return wizardLane;

            case 2:
                return rogueLane;

            case 3:
                return clericLane;

            case 4:
                return rangerLane;

            default:
                return warriorLane;
        }
    }

    public void LevelReset(int gold, int influence)
    {
        UpdateGoldCount(gold);
        UpdateInfluenceCount(influence);
    }

	public void UpdateInfluenceCount(int newCount)
    {
        influenceText.text = newCount.ToString();
    }

    public void UpdateGoldCount(int newCount)
    {
        goldText.text = newCount.ToString();
    }

    public void UpdateBossCurrentHealth(float newHealth)
    {
        bossHealthBar.UpdateValues(newHealth);
    }

    public void NewBoss(float maxHealth)
    {
        round++;
        roundText.text = round.ToString();

        bossHealthBar.NewValues(maxHealth);
    }

    public void NoBoss()
    {
        bossHealthBar.NewValues(0);
    }
        
    public void ResetLane(int lane)
    {
        switch (lane)
        {
            case 0:
                warriorLane.ResetValues();
                break;

            case 1:
                wizardLane.ResetValues();
                break;

            case 2:
                rogueLane.ResetValues();
                break;

            case 3:
                clericLane.ResetValues();
                break;

            case 4:
                rangerLane.ResetValues();
                break;
        }
    }
}
