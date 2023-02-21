using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientPlayer : MonoBehaviour
{
    public Sound.Names soundToPlay;

    [ContextMenu("Play Sound")]
    public void PlaySound()
    {
        if (!Application.isPlaying) return;
        AudioManager.S_PlayAmbientSound(soundToPlay);
    }
}
