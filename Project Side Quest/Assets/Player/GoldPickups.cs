using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickups : PickupClass {

    protected override void OnMouseDown()
    {
        pickupValue *= 20;
        gameManager.AddGold(pickupValue);

        base.OnMouseDown();
    }

}
