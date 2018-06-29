using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluencePickups : PickupClass {

    protected override void Pickup()
    {
        pickupValue *= 50;
        gameManager.AddInfluence(pickupValue);
    }

}
