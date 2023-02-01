using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpStation : StationBase
{
    public override void StartPrimaryInteract(object obj, ref Inventory inventory)
    {
        base.StartPrimaryInteract(obj, ref inventory);
        if (inventory)
        {
            // Give player item here
            throw new System.NotImplementedException("Not Implimented Give Item to player");
        }
    }

    public override void HoldPrimaryInteract(object obj, ref Inventory inventory)
    {
        base.HoldPrimaryInteract(obj, ref inventory);
    }

    public override void EndPrimaryInteract(object obj, ref Inventory inventory)
    {
        base.EndPrimaryInteract(obj, ref inventory);
    }
    public override void StartSecondaryInteract(ref Inventory inventory)
    {
        base.StartSecondaryInteract(ref inventory);
    }

    public override void HoldSecondaryInteract(ref Inventory inventory)
    {
        base.HoldSecondaryInteract(ref inventory);
    }

    public override void EndSecondaryInteract(ref Inventory inventory)
    {
        base.EndSecondaryInteract(ref inventory);
    }
}
