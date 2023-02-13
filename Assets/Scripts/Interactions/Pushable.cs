using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pushable : MonoBehaviour
{
    public UnityEvent OnPushStart;
    public UnityEvent OnPushEnd;
    public void PushStart()
    {
        OnPushStart?.Invoke();
    }
    public void Push(Vector3 direction)
    {
        transform.Translate(direction * Time.deltaTime);
    }
    public void PushEnd()
    {
        OnPushEnd?.Invoke();
    }
}
