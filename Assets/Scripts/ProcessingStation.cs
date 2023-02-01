using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingStation : StationBase
{
    [Header("Processing")]
    [SerializeField]
    private float _processingTime = 1f;
    private float _time = 0;

    public float ProcentageProgress => Mathf.Max(0,_time) / _processingTime;

    public override void StartPrimaryInteract(object obj, ref Inventory inventory)
    {
        base.StartPrimaryInteract(obj, ref inventory);
        if (obj == null && inventory)
        {
            // Give player item here
            throw new System.NotImplementedException("Not Implimented Give Item to player");
        }

        else if (obj != null)
        {
            // Start Processing Item
            if (!_isProcessingItem)
            {
                _processedItem = obj as ItemBase;
                StartCoroutine("ProcessingItem");
            }

        }
    }

    protected override IEnumerator ProcessingItem()
    {
        // getTime;
        _time = _processingTime;
        while (_time >= 0)
        {
            _time -= Time.deltaTime;
        }
        base.ProcessingItem();
        yield return 0;
    }
}
