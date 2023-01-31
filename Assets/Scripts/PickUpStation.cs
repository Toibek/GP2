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

    public override void HoldPrimaryInteract(object obj, ref PlayerController playerController)
    {
        base.HoldPrimaryInteract(obj, ref playerController);
    }

    public override void EndPrimaryInteract(object obj, ref PlayerController playerController)
    {
        base.EndPrimaryInteract(obj, ref playerController);
    }
    public override void StartSecondaryInteract(ref PlayerController playerController)
    {
        base.StartSecondaryInteract(ref playerController);
    }

    public override void HoldSecondaryInteract(ref PlayerController playerController)
    {
        base.HoldSecondaryInteract(ref playerController);
    }

    public override void EndSecondaryInteract(ref PlayerController playerController)
    {
        base.EndSecondaryInteract(ref playerController);
    }
}
