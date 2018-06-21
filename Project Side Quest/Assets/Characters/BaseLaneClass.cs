using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLaneClass : MonoBehaviour {

    private GameManager gameManager;

    private bool isAlive = true;

    private int characterCount = 1;
    private int upgradeLevel = 1;

    protected float totalHealth;
    protected float baseHealth = 20;
    protected float healthModifier = 1;
    protected float singleHealth;
    protected float currentHealth;

    protected float currentDamage;
    protected float baseDamage = 5;
    protected float damageModifier = 1;

    protected float currentResistance;
    protected float baseResistance = 5;
    protected float resistanceModifier = 1;

    protected float effectTimer = 0;
    private bool effectsEnabled = false;
    private int enabledEffect;

    protected float currentDodge;
    protected float baseDodge = 5;
    protected float dodgeModifier = 1;

    protected float currentSpeed = 1;
    protected float baseSpeed = 5;
    protected float attackSpeedModifier = 1;
    protected float attackTimer = 0;

    private int activeMobs = 0;

    private bool isFrozen = false;

    public virtual void Init(GameManager manager)
    {
        SetManager(manager);
        SetStats();
        isAlive = true;
    }

    private void Update()
    {
        if (isAlive)
        {
            if (!isFrozen)
            {
                attackTimer += Time.deltaTime;

                if (attackTimer >= currentSpeed)
                {
                    if (activeMobs > 0)
                    {
                        activeMobs -= (int)currentDamage;
                        gameManager.DropGold(currentDamage);

                        if (activeMobs < 0)
                            activeMobs = 0;
                    }
                    else
                    {
                        gameManager.DamageBoss(currentDamage);
                    }
                    attackTimer = 0;
                }
            }

            if(effectsEnabled)
            {
                effectTimer -= Time.deltaTime;

                if (effectTimer <= 0)
                    DisableEffects();
            }
        }
    }

    public void ResetStats()
    {
        Debug.Log("Stats Reset");
        
        healthModifier = 1;
        damageModifier = 1;
        resistanceModifier = 1;
        dodgeModifier = 1;
        attackSpeedModifier = 1;

        isAlive = true;
        attackTimer = 0;

        characterCount = 1;
        upgradeLevel = 1;

        SetStats();
    }

    public void LevelUp()
    {
        if (!isAlive)
            ResetStats();
        else
            SetStats();
    }

    public void Upgrade()
    {
        //Upgrade Character
        upgradeLevel++;

        healthModifier += 0.2f;
        damageModifier += 0.2f;
        resistanceModifier += 0.2f;
        dodgeModifier += 0.2f;
        attackSpeedModifier += 0.1f;

        totalHealth = (baseHealth * healthModifier) * characterCount;
        singleHealth = baseHealth * healthModifier;

        currentDamage = (baseDamage * damageModifier) * characterCount;

        currentResistance = baseResistance * resistanceModifier;

        currentDodge = baseDodge * dodgeModifier;

        currentSpeed = baseSpeed / attackSpeedModifier;
    }

    public void Employ()
    {
        //Employ Character
        characterCount++;
        
        currentDamage = (baseDamage * damageModifier) * characterCount;
        totalHealth = (baseHealth * healthModifier) * characterCount;
        currentHealth += baseHealth * healthModifier;

        currentDamage = (baseDamage * damageModifier) * characterCount;
    }

    public int GetCharacterCount()
    {
        return characterCount;
    }

    public int GetUpgradeLevel()
    {
        return upgradeLevel;
    }

    public float GetSingleHealth()
    {
        return singleHealth;
    }
    
    public bool Damage(float damage)
    {
        if (isAlive)
        {
            //Dodge Chance
            if (Random.Range(0, 99) > currentDodge)
            {

                Debug.Log("Lane Damaged");

                singleHealth -= damage;
                currentHealth -= damage;

                //Update Lane UI

                //CheckHealth();
                //CheckLaneStatus();

                return true;
            }
            else
            {
                Debug.Log("Lane Dodged!");
                return false;
            }
        }
        return false;

    }

    public void AddMobs(int mobCount)
    {
        if(isAlive)
            activeMobs += mobCount;
    }

    public bool CheckHealth()
    {
     characterCount--;
     singleHealth = baseHealth * healthModifier;
     currentDamage = (baseDamage * damageModifier) * characterCount;

     return CheckLaneStatus();
    }

    public bool CheckLaneStatus()
    {
        if(characterCount <= 0)
        {
            Debug.Log("Lane Destroyed");
            isAlive = false;
            return true;
        }

        return false;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    private void SetManager(GameManager manager)
    {
        gameManager = manager;
    }

    private void SetStats()
    {
        totalHealth = (baseHealth * healthModifier) * characterCount;
        currentHealth = totalHealth;
        singleHealth = baseHealth * healthModifier;

        currentDamage = (baseDamage * damageModifier) * characterCount;

        currentResistance = baseResistance * resistanceModifier;

        currentDodge = baseDodge * dodgeModifier;

        currentSpeed = baseSpeed / attackSpeedModifier;

        isAlive = true;        
    }

    public void Heal(float heal)
    {
        if (isAlive)
        {
            currentHealth += heal;
            singleHealth += heal;

            if (currentHealth >= totalHealth)
                currentHealth = totalHealth;

            if (singleHealth >= baseHealth * healthModifier)
                singleHealth = baseHealth * healthModifier;
        }
    }

    private void Effect()
    {
        switch(enabledEffect)
        {
            case 0:
                //Poison
                break;

            case 1:
                //Fire
                break;

            case 2:
                //Ice
                break;

            case 3:
                //Petrify
                isFrozen = true;
                break;

            default:
                break;
        }
    }

    public void EnableEffects(int effect, float time)
    {
        effectsEnabled = true;
        effectTimer = time - currentResistance;
        enabledEffect = effect;
    }

    private void DisableEffects()
    {
        effectsEnabled = false;

        switch (enabledEffect)
        {
            case 0:
                //Poison
                break;

            case 1:
                //Fire
                break;

            case 2:
                //Ice
                break;

            case 3:
                //Petrify
                isFrozen = false;
                break;

            default:
                break;
        }
    }

    protected virtual void Ability()
    {

    }

    private void OnMouseDown()
    {
        Ability();
    }
}
