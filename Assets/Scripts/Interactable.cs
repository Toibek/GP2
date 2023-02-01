using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void StartPrimaryInteract(object obj, ref Inventory inventory);// { }
    public abstract void HoldPrimaryInteract(object obj, ref Inventory inventory);// { }
    public abstract void EndPrimaryInteract(object obj, ref Inventory inventory);// { }

    public abstract void StartSecondaryInteract(ref Inventory inventory);// { }
    public abstract void HoldSecondaryInteract(ref Inventory inventory);// { }
    public abstract void EndSecondaryInteract(ref Inventory inventory);// { }
}
