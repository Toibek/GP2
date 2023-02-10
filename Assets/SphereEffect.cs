using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEffect : MonoBehaviour
{
    [SerializeField] private float effectTime;
    [SerializeField] private AnimationCurve curve;
    private float size;
    public void Run(float size)
    {
        this.size = size;
        StartCoroutine(Grow());
    }
    IEnumerator Grow()
    {
        for (float f = 0; f < effectTime; f += Time.deltaTime)
        {
            float s = curve.Evaluate(f / effectTime) * size;
            transform.localScale = new Vector3(s, s, s);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
