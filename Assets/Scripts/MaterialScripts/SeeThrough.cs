using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour
{
    private Material material;

    private void FixedUpdate()
    {
        if (material == null) material = GetComponent<Renderer>().material;

        if (material == null) return;

        if (GameManager.Instance.Player1 != null) Debug.Log("Player 1" + Camera.main.WorldToScreenPoint(GameManager.Instance.Player1.transform.position));
        if (GameManager.Instance.Player2 != null) Debug.Log("Player 2" + Camera.main.WorldToScreenPoint(GameManager.Instance.Player2.transform.position));

        Vector3 pos1 = GameManager.Instance.Player1 != null ? Camera.main.WorldToScreenPoint(GameManager.Instance.Player1.transform.position) : new Vector3(-1,-1,-1);
        Vector3 pos2 = GameManager.Instance.Player2 != null ? Camera.main.WorldToScreenPoint(GameManager.Instance.Player2.transform.position) : new Vector3(-1,-1,-1);

        material.SetVector("_Player1ScreenPosition", new Vector4(pos1.x / Display.main.renderingWidth, pos1.y / Display.main.renderingHeight, pos1.z, 0));
        material.SetVector("_Player2ScreenPosition", new Vector4(pos2.x / Display.main.renderingWidth, pos2.y / Display.main.renderingHeight, pos2.z, 0));


    }
}
