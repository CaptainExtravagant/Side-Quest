using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluencePickups : PickupClass {

    protected override void OnMouseDown()
    {
        pickupValue *= 50;
        gameManager.AddInfluence(pickupValue);

        base.OnMouseDown();
    }

}
