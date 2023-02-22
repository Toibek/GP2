using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [Header("Can use Both just call the playSound for dropdown and PlayRefSound for Fmod dropdown")]
    public Sound.Names soundToPlay;
    public FMODUnity.EventReference soundRefToPlay;
    
    public void PlaySound()
    {
        AudioManager.S_PlayOneShotSound(soundToPlay);
    }

    public void PlayRefSound()
    {
        AudioManager.S_PlayOneShotSound(soundRefToPlay);
    }
}
