using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositStation : StationBase
{
    public override void StartPrimaryInteract(object obj, ref Movement playerController)
    {
        base.StartPrimaryInteract(obj, ref playerController);
        if (obj.Equals(_inputItem))
        {
            // Give Check List a Check
        }
    }
}
