using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pushable : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public UnityEvent OnPushStart;
    public UnityEvent<Vector3> OnPush;
    public UnityEvent OnPushEnd;
    public void PushStart()
    {
        OnPushStart?.Invoke();
    }
    public void Push(Vector3 direction)
    {
        Vector3 dir = direction.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(dir);
        OnPush?.Invoke(dir);
    }
    public void PushEnd()
    {
        OnPushEnd?.Invoke();
    }
}
