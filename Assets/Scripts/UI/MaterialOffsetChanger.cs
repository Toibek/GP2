using UnityEngine;

public class MaterialOffsetChanger : MonoBehaviour
{
    // The interval at which the material offset should jump between 0 and 0.5
    public float jumpInterval = 0.5f;

    // The material to control the offset of
    public Material material;

    // The timer to track when to jump the material offset
    private float jumpTimer = 0.0f;

    void Update()
    {
        // Increment the jump timer based on the time elapsed since the last frame
        jumpTimer += Time.deltaTime;

        // If the jump interval has elapsed, jump the material offset and reset the jump timer
        if (jumpTimer >= jumpInterval)
        {
            // Reset the jump timer
            jumpTimer = 0.0f;

            // Jump the material offset between 0 and 0.5
            float newOffset = material.mainTextureOffset.x == 0.0f ? 0.5f : 0.0f;
            material.mainTextureOffset = new Vector2(newOffset, 0.0f);
        }
    }
}