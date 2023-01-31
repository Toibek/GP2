using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBase : Interactable
{
    [Header("Base Station")]
    [SerializeField]
    protected ItemBase _inputItem;
    [SerializeField]
    protected ItemBase _processedItem;
    [SerializeField]
    protected ItemBase _outputItem;

    protected bool isProcessingItem;
    public override void StartPrimaryInteract(object obj, ref Movement playerController)
    protected bool _isProcessingItem;
    public override void StartPrimaryInteract(object obj, ref PlayerController playerController)
    {
        throw new System.NotImplementedException();
    }

    public override void EndPrimaryInteract(object obj, ref Movement playerController)
    {
        throw new System.NotImplementedException();
    }

    public override void HoldPrimaryInteract(object obj, ref Movement playerController)
    {
        throw new System.NotImplementedException();
    }

    public override void StartSecondaryInteract(ref Movement playerController)
    {
        throw new System.NotImplementedException();
    }

    public override void HoldSecondaryInteract(ref Movement playerController)
    {
        throw new System.NotImplementedException();
    }

    public override void EndSecondaryInteract(ref Movement playerController)
    {
        throw new System.NotImplementedException();
    }

    protected virtual IEnumerator ProcessingItem()
    {
        if (_isProcessingItem) _isProcessingItem = false;
        yield return 0;
    }

}
