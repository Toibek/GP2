using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovingPlatformRotation : MonoBehaviour
{
    private FreeMovingPlatform _freeMovingPlatform;
    private float rotation;

    public void Rotate()
    {
        _freeMovingPlatform.Rotate(rotation);
    }
}
