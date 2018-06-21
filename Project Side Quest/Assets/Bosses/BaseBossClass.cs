﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossClass : MonoBehaviour {

    //Game Manager Variable
    protected GameManager gameManager;

    protected float baseHealth = 100;
    protected float healthModifier = 1.0f;

    private float currentHealth;
    private float maxHealth;

    protected float baseAttack = 10;
    protected float attackModifier = 1.0f;

    private float currentAttack;

    protected float attackSpeed = 5;
    protected float attackTimer = 0;

    protected float specialSpeed = 30;
    protected float specialTimer = 0;

    protected float specialTime = 10;
    protected float specialModifier;

    private float currentSpecial;

    protected int targetLane;

    protected int damageEffect = 0;

    private void Start()
    {
        SetStats();
    }

    // Update is called once per frame
    void Update () {
        attackTimer += Time.deltaTime;
        specialTimer += Time.deltaTime;

        if(attackTimer >= attackSpeed)
        {
            AttackLane();
            attackTimer = 0;
        }

        if(specialTimer >= specialSpeed)
        {
            SpecialAttack();
            specialTimer = 0;
        }
	}

    public virtual void Init(GameManager manager)
    {
        SetGameManager(manager);

        SetModifiers(gameManager.GetCurrentLevel());
        SetStats();
    }

    private void OnMouseDown()
    {
        Debug.Log("Player Damage Boss");
        TakeDamage(10);
    }

    public void SetModifiers(int level)
    {
        attackModifier += 0.2f * level;
        healthModifier += 0.2f * level;
        specialModifier += 0.2f * level;
    }

    public void SetStats()
    {
        currentHealth = baseHealth * healthModifier;
        maxHealth = currentHealth;

        currentAttack = baseAttack * attackModifier;
        currentSpecial = specialTime * specialModifier;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    void SetTargetLane()
    {
        targetLane = Random.Range(0, 4);
    }

    void AttackLane()
    {
        SetTargetLane();
        gameManager.DamageLane(targetLane, currentAttack);
    }

    public void TakeDamage(float damage)
    {        
        currentHealth -= damage;

        gameManager.UpdateBossHealth();

        if (CheckHealth())
            gameManager.BossDefeated();
    }

    private bool CheckHealth()
    {
        if (currentHealth <= 0)
            return true;
        else
            return false;
    }

    protected void Heal(float healValue)
    {
        currentHealth += healValue;

        if (currentHealth >= baseHealth * healthModifier)
            currentHealth = baseHealth * healthModifier;
    }

    virtual protected void SpecialAttack()
    {
        SetTargetLane();
        gameManager.LaneEffect(targetLane, damageEffect, currentSpecial);
    }
}
