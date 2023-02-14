using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MovingObject : MonoBehaviour
{
    [Header("Moving object")]
    [SerializeField] Vector3 moveTo;
    [SerializeField] float moveTime;
    [SerializeField] AnimationCurve moveCurve;
    [SerializeField] UnityEvent OnMoveStart;
    [SerializeField] UnityEvent OnMoveDone;
    bool moveToGoal;


    Vector3 originalPosition;
    Vector3 goalPosition;
    Coroutine moving;
    float time;
    private void Start()
    {
        originalPosition = transform.position;
        goalPosition = originalPosition + moveTo;
    }
    [ContextMenu("Switch")]
    public void Switch()
    {
        moveToGoal = !moveToGoal;
        if (moving == null)
            moving = StartCoroutine(move());
    }
    [ContextMenu("To goal")]
    public void ToGoal()
    {
        moveToGoal = true;
        if (moving == null)
            moving = StartCoroutine(move());
    }
    [ContextMenu("To start")]
    public void ToStart()
    {
        moveToGoal = false;
        if (moving == null)
            moving = StartCoroutine(move());
    }
    IEnumerator move()
    {
        OnMoveStart?.Invoke();
        while (time >= 0 && time <= moveTime)
        {
            if (moveToGoal) time += Time.deltaTime;
            else time -= Time.deltaTime;

            float v = moveCurve.Evaluate(time / moveTime);
            transform.position = Vector3.MoveTowards(originalPosition, goalPosition, Vector3.Distance(originalPosition, goalPosition) * v);

            yield return new WaitForEndOfFrame();
        }

        if (time <= 0)
        {
            transform.position = originalPosition;
            time = 0;
        }
        else if (time >= moveTime)
        {
            transform.position = goalPosition;
            time = moveTime;
        }

        OnMoveDone?.Invoke();
        moving = null;
    }
}
