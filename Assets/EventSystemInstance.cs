using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemInstance : MonoBehaviour
{
    public static EventSystemInstance Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
