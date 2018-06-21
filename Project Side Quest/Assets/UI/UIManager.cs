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

    private GameObject warriorLane;
    private GameObject wizardLane;
    private GameObject rogueLane;
    private GameObject clericLane;
    private GameObject rangerLane;

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
        warriorLane = GetComponentsInChildren<VerticalLayoutGroup>()[0].gameObject;        
        wizardLane = GetComponentsInChildren<VerticalLayoutGroup>()[1].gameObject;
        rogueLane = GetComponentsInChildren<VerticalLayoutGroup>()[2].gameObject;
        clericLane = GetComponentsInChildren<VerticalLayoutGroup>()[3].gameObject;
        rangerLane = GetComponentsInChildren<VerticalLayoutGroup>()[4].gameObject;

        //Init Lane Health Bars
        warriorLane.GetComponentInChildren<HeroHealthBar>().Init();
        warriorLane.GetComponentInChildren<HeroHealthBar>().NewValues(manager.GetLane(0).GetSingleHealth());
        wizardLane.GetComponentInChildren<HeroHealthBar>().Init();
        wizardLane.GetComponentInChildren<HeroHealthBar>().NewValues(manager.GetLane(1).GetSingleHealth());
        rogueLane.GetComponentInChildren<HeroHealthBar>().Init();
        rogueLane.GetComponentInChildren<HeroHealthBar>().NewValues(manager.GetLane(2).GetSingleHealth());
        clericLane.GetComponentInChildren<HeroHealthBar>().Init();
        clericLane.GetComponentInChildren<HeroHealthBar>().NewValues(manager.GetLane(3).GetSingleHealth());
        rangerLane.GetComponentInChildren<HeroHealthBar>().Init();
        rangerLane.GetComponentInChildren<HeroHealthBar>().NewValues(manager.GetLane(4).GetSingleHealth());

        //Set Lane Upgrade Values
        warriorLane.GetComponentsInChildren<Text>()[0].text = (manager.GetLane(0).GetCharacterCount() * 200).ToString();
        warriorLane.GetComponentsInChildren<Text>()[1].text = (manager.GetLane(0).GetUpgradeLevel() * 100).ToString();

        wizardLane.GetComponentsInChildren<Text>()[0].text = (manager.GetLane(1).GetCharacterCount() * 200).ToString();
        wizardLane.GetComponentsInChildren<Text>()[1].text = (manager.GetLane(1).GetUpgradeLevel() * 100).ToString();

        rogueLane.GetComponentsInChildren<Text>()[0].text = (manager.GetLane(2).GetCharacterCount() * 200).ToString();
        rogueLane.GetComponentsInChildren<Text>()[1].text = (manager.GetLane(2).GetUpgradeLevel() * 100).ToString();

        clericLane.GetComponentsInChildren<Text>()[0].text = (manager.GetLane(3).GetCharacterCount() * 200).ToString();
        clericLane.GetComponentsInChildren<Text>()[1].text = (manager.GetLane(3).GetUpgradeLevel() * 100).ToString();

        rangerLane.GetComponentsInChildren<Text>()[0].text = (manager.GetLane(4).GetCharacterCount() * 200).ToString();
        rangerLane.GetComponentsInChildren<Text>()[1].text = (manager.GetLane(4).GetUpgradeLevel() * 100).ToString();

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

    public void UpdateLaneValue(int lane, int currency, int newValue)
    {
        switch(lane)
        {
            case 0:
                //Warrior
                warriorLane.GetComponentsInChildren<Text>()[currency].text = newValue.ToString();
                break;

            case 1:
                //Wizard
                wizardLane.GetComponentsInChildren<Text>()[currency].text = newValue.ToString();
                break;

            case 2:
                //Rogue
                rogueLane.GetComponentsInChildren<Text>()[currency].text = newValue.ToString();
                break;

            case 3:
                //Cleric
                clericLane.GetComponentsInChildren<Text>()[currency].text = newValue.ToString();
                break;

            case 4:
                //Ranger
                rangerLane.GetComponentsInChildren<Text>()[currency].text = newValue.ToString();
                break;
        }
    }

    public void UpdateLaneHealth(int lane, float newValue)
    {
        switch(lane)
        {
            case 0:
                warriorLane.GetComponentInChildren<HeroHealthBar>().UpdateValues(newValue);
                break;

            case 1:
                wizardLane.GetComponentInChildren<HeroHealthBar>().UpdateValues(newValue);
                break;

            case 2:
                rogueLane.GetComponentInChildren<HeroHealthBar>().UpdateValues(newValue);
                break;

            case 3:
                clericLane.GetComponentInChildren<HeroHealthBar>().UpdateValues(newValue);
                break;

            case 4:
                rangerLane.GetComponentInChildren<HeroHealthBar>().UpdateValues(newValue);
                break;
        }
    }

    public void LaneCharacterLost(int lane, bool laneLost)
    {
        switch (lane)
        {
            case 0:
                warriorLane.GetComponentInChildren<HeroHealthBar>().CharacterKilled(laneLost);
                break;

            case 1:
                wizardLane.GetComponentInChildren<HeroHealthBar>().CharacterKilled(laneLost);
                break;

            case 2:
                rogueLane.GetComponentInChildren<HeroHealthBar>().CharacterKilled(laneLost);
                break;

            case 3:
                clericLane.GetComponentInChildren<HeroHealthBar>().CharacterKilled(laneLost);
                break;

            case 4:
                rangerLane.GetComponentInChildren<HeroHealthBar>().CharacterKilled(laneLost);
                break;
        }
    }

    public void LaneUpgraded(int lane, int level, float newValue)
    {
        switch (lane)
        {
            case 0:
                warriorLane.GetComponentInChildren<HeroHealthBar>().LevelUp(newValue);

                break;

            case 1:
                wizardLane.GetComponentInChildren<HeroHealthBar>().LevelUp(newValue);

                break;

            case 2:
                rogueLane.GetComponentInChildren<HeroHealthBar>().LevelUp(newValue);

                break;

            case 3:
                clericLane.GetComponentInChildren<HeroHealthBar>().LevelUp(newValue);

                break;

            case 4:
                rangerLane.GetComponentInChildren<HeroHealthBar>().LevelUp(newValue);

                break;
        }
    }
    
    public void LaneEmployed(int lane)
    {
        switch (lane)
        {
            case 0:
                warriorLane.GetComponentInChildren<HeroHealthBar>().CharacterCountUp();
                break;

            case 1:
                wizardLane.GetComponentInChildren<HeroHealthBar>().CharacterCountUp();
                break;

            case 2:
                rogueLane.GetComponentInChildren<HeroHealthBar>().CharacterCountUp();
                break;

            case 3:
                clericLane.GetComponentInChildren<HeroHealthBar>().CharacterCountUp();
                break;

            case 4:
                rangerLane.GetComponentInChildren<HeroHealthBar>().CharacterCountUp();
                break;
        }
    }

    public void ResetLane(int lane)
    {
        switch (lane)
        {
            case 0:
                warriorLane.GetComponentInChildren<HeroHealthBar>().ResetValues();
                break;

            case 1:
                wizardLane.GetComponentInChildren<HeroHealthBar>().ResetValues();
                break;

            case 2:
                rogueLane.GetComponentInChildren<HeroHealthBar>().ResetValues();
                break;

            case 3:
                clericLane.GetComponentInChildren<HeroHealthBar>().ResetValues();
                break;

            case 4:
                rangerLane.GetComponentInChildren<HeroHealthBar>().ResetValues();
                break;
        }
    }
}
