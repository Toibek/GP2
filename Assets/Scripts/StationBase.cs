using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationBase : Interactable
{
    protected ItemBase _inputItem;
    protected ItemBase _proccesedItem;
    protected ItemBase _outputItem;

    protected bool isProcessingItem;
    public override void StartPrimaryInteract(object obj, ref PlayerController playerController)
    {
        //if (obj != null)
        //{
        //    ItemBase _item = obj as ItemBase;
        //    if (_item != null)
        //    {
        //        _inputItem = _item;
        //    }
        //}
        //else 
        //{
        //    return;
        //}
    }

    public override void EndPrimaryInteract(object obj, ref PlayerController playerController)
    {

    }

    public override void HoldPrimaryInteract(object obj, ref PlayerController playerController)
    {

    }

    public override void StartSecondaryInteract(ref PlayerController playerController)
    {
        throw new System.NotImplementedException();
    }

    public override void HoldSecondaryInteract(ref PlayerController playerController)
    {
        throw new System.NotImplementedException();
    }

    public override void EndSecondaryInteract(ref PlayerController playerController)
    {
        throw new System.NotImplementedException();
    }

    protected virtual IEnumerator ProcessingItem()
    {
        if (isProcessingItem) isProcessingItem = false;
        yield return 0;
    }
}
