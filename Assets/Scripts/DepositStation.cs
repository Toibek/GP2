using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositStation : StationBase
{
    public override void StartPrimaryInteract(object obj, ref Inventory inventory)
    {
        base.StartPrimaryInteract(obj, ref inventory);
        if (obj.Equals(_inputItem))
        {
            // Give Check List a Check
        }
    }
}
