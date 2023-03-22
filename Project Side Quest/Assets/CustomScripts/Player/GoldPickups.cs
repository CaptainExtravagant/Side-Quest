using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickups : PickupClass {

    protected override void Pickup()
    {
        pickupValue *= 20;
        gameManager.AddGold(pickupValue);        
    }

}
