using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickups : PickupClass {

    protected override void OnMouseDown()
    {
        gameManager.AddGold(pickupValue);
    }

}
