using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void StartPrimaryInteract(object obj, ref PlayerController playerController);// { }
    public abstract void HoldPrimaryInteract(object obj, ref PlayerController playerController);// { }
    public abstract void EndPrimaryInteract(object obj, ref PlayerController playerController);// { }

    public abstract void StartSecondaryInteract(ref PlayerController playerController);// { }
    public abstract void HoldSecondaryInteract(ref PlayerController playerController);// { }
    public abstract void EndSecondaryInteract(ref PlayerController playerController);// { }
}
