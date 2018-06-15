using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluencePickups : PickupClass {

    protected override void OnMouseDown()
    {
        gameManager.AddInfluence(pickupValue);
    }

}
