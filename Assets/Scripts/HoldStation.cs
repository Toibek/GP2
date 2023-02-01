using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldStation : StationBase
{
    
    [Header("Hold")]
    [SerializeField]
    private float _holdAmount = 3f;
    private float _heldAmount;
    [SerializeField] [Tooltip("Will my progress reset when I stop holding down")]
    private bool _resetsProgress;
    [SerializeField]
    private UnityEngine.Events.UnityEvent OnFinishedHold;

    private void Awake()
    {
        _heldAmount = _holdAmount;
    }

    public override void StartPrimaryInteract(object obj, ref Inventory inventory)
    {
        base.StartPrimaryInteract(obj, ref inventory);
    }

    public override void HoldPrimaryInteract(object obj, ref Inventory inventory)
    {
        base.HoldPrimaryInteract(obj, ref inventory);
        _heldAmount -= Time.deltaTime;

        if (_heldAmount >= _holdAmount)
        {
            OnFinishedHold.Invoke();
            _heldAmount = _holdAmount;
        }
    }

    public override void EndPrimaryInteract(object obj, ref Inventory inventory)
    {
        base.EndPrimaryInteract(obj, ref inventory);
        if (_resetsProgress) _heldAmount = _holdAmount;
    }

    private void OnValidate()
    {
        _heldAmount = _holdAmount;
    }
}
