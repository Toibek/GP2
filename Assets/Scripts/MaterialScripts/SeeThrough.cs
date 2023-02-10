using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour
{
    private Material material;

    private RaycastHit[] hits = new RaycastHit[10];

    private void FixedUpdate()
    {
        if (Physics.RaycastNonAlloc(Camera.main.transform.position, transform.position - Camera.main.transform.position, hits) > 0)
        {

            for (int i = 0; i < hits.Length; i++)
            {
                try
                {
                    if (hits[i].transform == null) continue;
                }
                catch
                {
                    break;
                }

                if (material == null) material = hits[i].transform.GetComponent<Renderer>().material;

                if (material == null) continue;

                if (GameManager.Instance.Player1 != null) Debug.Log("Player 1" + Camera.main.WorldToScreenPoint(GameManager.Instance.Player1.transform.position));
                if (GameManager.Instance.Player2 != null) Debug.Log("Player 2" + Camera.main.WorldToScreenPoint(GameManager.Instance.Player2.transform.position));

                Vector3 ScreenPos1 = GameManager.Instance.Player1 != null ? Camera.main.WorldToScreenPoint(GameManager.Instance.Player1.transform.position) : new Vector3(-1, -1, -1);
                Vector3 ScreenPos2 = GameManager.Instance.Player2 != null ? Camera.main.WorldToScreenPoint(GameManager.Instance.Player2.transform.position) : new Vector3(-1, -1, -1);

                Vector3 pos1 = GameManager.Instance.Player1 != null ? GameManager.Instance.Player1.transform.position : Vector3.zero;
                Vector3 pos2 = GameManager.Instance.Player2 != null ? GameManager.Instance.Player2.transform.position : Vector3.zero;

                material.SetVector("_Player1ScreenPosition", new Vector4(ScreenPos1.x / Display.main.renderingWidth, ScreenPos1.y / Display.main.renderingHeight, ScreenPos1.z, 0));
                material.SetVector("_Player2ScreenPosition", new Vector4(ScreenPos2.x / Display.main.renderingWidth, ScreenPos2.y / Display.main.renderingHeight, ScreenPos2.z, 0));

                material.SetVector("_Player1Position", new Vector4(pos1.x, pos1.y - 1f, pos1.z, 0));
                material.SetVector("_Player2Position", new Vector4(pos2.x, pos2.y - 1f, pos2.z, 0));

            }

        }


    }
}
