using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpStation : StationBase
{
    public override void StartPrimaryInteract(object obj, ref PlayerController playerController)
    {
        base.StartPrimaryInteract(obj, ref playerController);
        if (playerController)
        {
            // Give player item here
            throw new System.NotImplementedException("Not Implimented Give Item to player");
        }
    }
}
