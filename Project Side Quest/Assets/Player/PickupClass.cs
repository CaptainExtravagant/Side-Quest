﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupClass : MonoBehaviour {

	protected int pickupValue;
    protected GameManager gameManager;

	public void Init(int currentLevel, GameManager manager)
	{
        gameManager = manager;

        pickupValue = Random.Range(1, 10) * (currentLevel + 1);
	}

    private void OnMouseDown()
    {
        Pickup();
        Destroy(gameObject);
    }

    protected virtual void Pickup()
    {

    }

}
