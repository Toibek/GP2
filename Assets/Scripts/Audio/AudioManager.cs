using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script creates a singleton instance of the "AudioManager" so that it can be accessed from anywhere in the scene, and plays audio clips based on their names.
/// </summary>

public class AudioManager : MonoBehaviour
{
    // Singleton instance of the AudioManager
    public static AudioManager Instance;

    // An array to store audio clips
    public Sound[] Sounds;

    // The AudioSource component used to play audio clips
    public AudioSource AudioSource;

    private void Awake()
    {
        // Ensure that there is only one instance of the AudioManager
        if (Instance == null)
        {
            Instance = this;
        }

        // Loop through all the audio clips in the "Sounds" array
        foreach (Sound sound in Sounds)
        {
            // Add an AudioSource component to the gameObject
            sound.source = gameObject.AddComponent<AudioSource>();
            // Assign the audio clip and properties to the newly added AudioSource component
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    // Function to play audio clips by name
    public void PlaySound(string name)
    {
        // Loop through all the audio clips in the "Sounds" array
        foreach (Sound sound in Sounds)
        {
            // If the name of the audio clip matches the input name
            if (sound.name == name)
            {
                // Play the audio clip
                sound.source.Play();
            }
        }
    }
}