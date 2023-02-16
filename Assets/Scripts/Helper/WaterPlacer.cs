using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlacer : MonoBehaviour
{
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private Vector2Int grid;

    [SerializeField] private float waterSize;
    private List<GameObject> waters = new List<GameObject>();

    [ContextMenu("Generate Water")]
    public void GenerateWater()
    {
        if (grid.x <= 0 || grid.y <= 0) return;
        if (waters != null && waters.Count > 0)
            for (int i = waters.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(waters[i]);
            }

        waters = new List<GameObject>();
        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            {
                waters.Add(Instantiate(waterPrefab, transform.position + new Vector3(waterSize * x, 0, waterSize * y), Quaternion.identity, transform));
            }
        }
    }
}
