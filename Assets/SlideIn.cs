using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideIn : MonoBehaviour
{
    [SerializeField] Vector2 AnchoredStart;
    [SerializeField] Vector2 AnchoredGoal;
    [SerializeField] float time;
    [SerializeField] AnimationCurve curve;
    Coroutine movingRoutine;
    RectTransform rt;
    private void OnEnable()
    {
        Debug.Log("EnablingPause");
        rt = GetComponent<RectTransform>();
        if (movingRoutine == null) movingRoutine = StartCoroutine(MoveEnum());
    }
    private void OnDisable()
    {
        Debug.Log("DisablingPause");
        rt.anchoredPosition = AnchoredStart;
    }
    IEnumerator MoveEnum()
    {
        for (float f = 0; f < time; f += Time.unscaledDeltaTime)
        {
            float v = curve.Evaluate(f / time);
            rt.anchoredPosition = Vector2.Lerp(AnchoredStart, AnchoredGoal, v);
            yield return new WaitForEndOfFrame();
        }
        movingRoutine = null;
    }
}
