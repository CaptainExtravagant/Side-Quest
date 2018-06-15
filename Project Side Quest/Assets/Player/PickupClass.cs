using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupClass : MonoBehaviour {

	protected int pickupValue;
    protected GameManager gameManager;

	public void Init(int currentLevel, GameManager manager)
	{
        gameManager = manager;

        pickupValue = Random.Range(20, 40) * currentLevel;
	}

    protected virtual void OnMouseDown()
    {
    }

}
