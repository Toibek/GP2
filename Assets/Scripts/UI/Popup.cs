using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Popup : MonoBehaviour
{
    [Header("Textures")]
    [SerializeField] private List<Texture> ControllerImages;
    [SerializeField] private List<Texture> KeyboardImages;
    [Header("Settings")]
    public float jumpInterval = 0.5f;

    private List<Texture> activeTextures;
    private MeshRenderer mr;
    Coroutine SwitchImageRoutine;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        string input = GetComponentInParent<PlayerInput>().currentControlScheme;
        if (input.Contains("Keyboard")) activeTextures = KeyboardImages;
        else activeTextures = ControllerImages;
    }
    public void Show(int i) => Show(activeTextures[i]);
    public void Show(Texture texture)
    {
        mr.material.mainTexture = texture;
        if (SwitchImageRoutine == null) StartCoroutine(SwitchImageEnum());
        mr.enabled = true;
    }
    public void Hide()
    {
        mr.enabled = false;
    }
    IEnumerator SwitchImageEnum()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpInterval);
            // Jump the material offset between 0 and 0.5
            float newOffset = mr.material.mainTextureOffset.x == 0.0f ? 0.5f : 0.0f;
            mr.material.mainTextureOffset = new Vector2(newOffset, 0.0f);
        }
    }
}