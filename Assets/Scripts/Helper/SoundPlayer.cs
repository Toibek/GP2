using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public Sound.Names soundToPlay;
    
    public void PlaySound()
    {
        AudioManager.S_PlayOneShotSound(soundToPlay);
    }
}
