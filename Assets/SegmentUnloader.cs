using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SegmentUnloader : MonoBehaviour
{
    [SerializeField] List<UnloadPoint> unloadPoints;
    int activeUnloadPoint;
    Transform p1;
    Transform p2;

    private void Update()
    {
        if (p1 == null && GameManager.Instance.Player1 != null)
            p1 = GameManager.Instance.Player1.transform;
        else if (p2 == null && GameManager.Instance.Player2 != null)
            p2 = GameManager.Instance.Player2.transform;
        else if (p1 != null && p2 != null && activeUnloadPoint < unloadPoints.Count)
        {
            UnloadPoint up = unloadPoints[activeUnloadPoint];
            if (p1.position.z > up.ZDistance && p2.position.z > up.ZDistance)
            {
                up.Segment.SetActive(false);
                up.OnUnload?.Invoke();
                activeUnloadPoint++;
            }
        }
    }
}
[System.Serializable]
public class UnloadPoint : object
{
    public float ZDistance;
    public GameObject Segment;
    public UnityEvent OnUnload;
}
