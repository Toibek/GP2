using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveOnCount : MovingObject
{
    [Header("Move on count")]
    [SerializeField] private int expectedCount;
    [SerializeField] private UnityEvent OnCorrectCount;
    [SerializeField] private UnityEvent OnIncorrectCount;
    bool wasCorrect;
    public void SubmitCount(int count)
    {
        if (count >= expectedCount && !wasCorrect)
        {
            wasCorrect = true;
            OnCorrectCount?.Invoke();
            ToGoal();
        }
        else if (count < expectedCount && wasCorrect)
        {
            wasCorrect = false;
            OnIncorrectCount?.Invoke();
            ToStart();
        }
    }
}
