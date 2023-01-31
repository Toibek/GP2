using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void StartPrimaryInteract(object obj, ref Movement playerController);// { }
    public abstract void HoldPrimaryInteract(object obj, ref Movement playerController);// { }
    public abstract void EndPrimaryInteract(object obj, ref Movement playerController);// { }

    public abstract void StartSecondaryInteract(ref Movement playerController);// { }
    public abstract void HoldSecondaryInteract(ref Movement playerController);// { }
    public abstract void EndSecondaryInteract(ref Movement playerController);// { }
}
