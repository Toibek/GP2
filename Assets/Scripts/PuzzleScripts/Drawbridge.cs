using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Drawbridge : MonoBehaviour
{
    [Header("Moving object")]
    [SerializeField] Vector3 EulerGoal;
    [SerializeField] float moveTime;
    [SerializeField] AnimationCurve moveCurve;
    [SerializeField] UnityEvent OnMoveStart;
    [SerializeField] UnityEvent<bool> OnMoveDone;
    bool moveToGoal;


    Quaternion originalRotation;
    Quaternion goalRotation;
    Coroutine moving;
    float time;
    private void Start()
    {
        originalRotation = transform.rotation;
        goalRotation = Quaternion.Euler(originalRotation.eulerAngles + EulerGoal);
    }
    [ContextMenu("Switch")]
    public void Switch()
    {
        moveToGoal = !moveToGoal;
        if (moving == null)
            moving = StartCoroutine(Move());
    }
    [ContextMenu("To goal")]
    public void ToGoal()
    {
        moveToGoal = true;
        if (moving == null)
            moving = StartCoroutine(Move());
    }
    [ContextMenu("To start")]
    public void ToStart()
    {
        moveToGoal = false;
        if (moving == null)
            moving = StartCoroutine(Move());
    }
    public void SetMove(bool toGoal)
    {
        moveToGoal = toGoal;
        if (moving == null)
            moving = StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        OnMoveStart?.Invoke();
        while (time >= 0 && time <= moveTime)
        {
            if (moveToGoal) time += Time.deltaTime;
            else time -= Time.deltaTime;

            float v = moveCurve.Evaluate(time / moveTime);
            transform.rotation = Quaternion.RotateTowards(originalRotation, goalRotation, EulerGoal.magnitude * v);

            yield return new WaitForEndOfFrame();
        }

        if (time <= 0)
        {
            transform.rotation = originalRotation;
            time = 0;
        }
        else if (time >= moveTime)
        {
            transform.rotation = goalRotation;
            time = moveTime;
        }

        OnMoveDone?.Invoke(moveToGoal);
        moving = null;
    }
}
