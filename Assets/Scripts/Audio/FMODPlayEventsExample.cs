using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class FMODPlayEventsExample : MonoBehaviour
{
    // The class "FMODUNITY.EventReference" is used to locate a sound
    // Can either be serialized or set in code (if you know where the sound is located)
    // Example: "event:/UI/SelectNegative"
    [SerializeField] private FMODUnity.EventReference SoundToPlay;


    void Start()
    {
        //Plays the sound once, then removes it from memory
        FMODUnity.RuntimeManager.PlayOneShot(SoundToPlay);
        
        //Attaches the sound to game object and plays it, then removes it from memory
        //FMODUnity.RuntimeManager.PlayOneShotAttached(SoundToPlay, this.gameObject);
    }
    
}
