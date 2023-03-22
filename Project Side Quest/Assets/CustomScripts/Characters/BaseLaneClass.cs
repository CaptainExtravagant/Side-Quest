using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLaneClass : MonoBehaviour {

    [SerializeField]
    private CharacterData CharacterData;

    protected GameManager gameManager;
    private LaneUI laneUI;

    //Animation Variables
    private Animator animator;
    private int animationState = 0;

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

    protected bool abilityCooldown = false;
    private float abilityTimer;

    public virtual void Init(GameManager manager, LaneUI ui)
    {
        SetManager(manager);

        animator = GetComponent<Animator>();

        laneUI = ui;
        SetStats();
        abilityTimer = currentSpeed;
        isAlive = true;
    }    

    private void Update()
    {
        if (gameManager.IsGamePaused() == false)
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
                            DamageMobs((int)currentDamage);
                        }
                        else
                        {
                            animator.SetTrigger("Attack");
                            gameManager.DamageBoss(currentDamage);
                        }
                        attackTimer = 0;
                    }

                    if (abilityCooldown)
                    {
                        abilityTimer -= Time.deltaTime;
                        laneUI.UpdateCooldown(abilityTimer / currentSpeed);
                        if (abilityTimer <= 0)
                        {
                            abilityCooldown = false;
                            abilityTimer = currentSpeed;
                        }
                    }
                }


                if (effectsEnabled)
                {
                    effectTimer -= Time.deltaTime;

                    if (effectTimer <= 0)
                        DisableEffects();
                }
            }
        }
    }

    public void DamageMobs(int damage)
    {
        activeMobs -= damage;
        gameManager.DropGold(damage);

        if (activeMobs < 0)
            activeMobs = 0;
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

        laneUI.UpdateCurrencyValues(GameManager.CURRENCY.GOLD, GetUpgradeLevel() * 100);
        laneUI.UpdateValues(GetSingleHealth());
        laneUI.LevelUp(GetUpgradeLevel());
    }

    public void Employ()
    {
        //Employ Character
        characterCount++;
        
        currentDamage = (baseDamage * damageModifier) * characterCount;
        totalHealth = (baseHealth * healthModifier) * characterCount;
        currentHealth += baseHealth * healthModifier;

        currentDamage = (baseDamage * damageModifier) * characterCount;

        laneUI.UpdateCurrencyValues(GameManager.CURRENCY.INFLUENCE, GetCharacterCount() * 200);
        laneUI.CharacterCountUp();
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
    
    public void Damage(float damage)
    {
        if (isAlive)
        {
            //Dodge Chance
            if (Random.Range(0, 99) > currentDodge)
            {
                animator.SetTrigger("Hit");

                Debug.Log("Lane Damaged");

                singleHealth -= damage;
                currentHealth -= damage;

                //Update Lane UI
                CheckHealth();

                laneUI.UpdateValues(GetSingleHealth());
            }
            else
            {
                Debug.Log("Lane Dodged!");
            }
        }

    }

    public void AddMobs(int mobCount)
    {
        if(isAlive)
            activeMobs += mobCount;
    }

    public void CheckHealth()
    {
        if (singleHealth <= 0)
        {
            characterCount--;
            singleHealth = baseHealth * healthModifier;
            currentDamage = (baseDamage * damageModifier) * characterCount;

            laneUI.CharacterKilled(CheckLaneStatus());
        }
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

        laneUI.NewValues(GetSingleHealth());
        
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
        Debug.Log("Ability Used");
        if (!abilityCooldown)
        {
            animator.SetTrigger("SP_Attack");
            Ability();
            abilityCooldown = true;
            
        }
    }
}
