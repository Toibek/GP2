using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovingPlatformRotation : MonoBehaviour
{
    [SerializeField]
    private FreeMovingPlatform _freeMovingPlatform;
    [SerializeField]
    private float rotation = 90f;

    public void Rotate()
    {
        if (_freeMovingPlatform != null)
            _freeMovingPlatform.Rotate(rotation);
    }
}
