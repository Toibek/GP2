using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] Vector3 moveTo;
    [SerializeField] float moveTime;
    [SerializeField] AnimationCurve moveCurve;
    Vector3 originalPosition;
    Coroutine moving;
    private void Start()
    {
        originalPosition = transform.position;
    }
    [ContextMenu("Run")]
    public void Run()
    {
        if (moving == null)
            moving = StartCoroutine(move());
    }
    IEnumerator move()
    {
        Vector3 start;
        Vector3 goal;
        if (transform.position == originalPosition)
        {
            start = originalPosition;
            goal = originalPosition + moveTo;
        }
        else
        {
            start = originalPosition + moveTo;
            goal = originalPosition;
        }
        Debug.Log(start + "=>" + goal);
        for (float f = 0; f < moveTime; f += Time.deltaTime)
        {
            float v = moveCurve.Evaluate(f / moveTime);
            transform.position = Vector3.MoveTowards(start, goal, Vector3.Distance(start, goal) * v);
            yield return new WaitForEndOfFrame();
        }

        transform.position = goal;
        moving = null;
    }
}
