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
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound sound in Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    // Function to play audio clips by index
    public void PlaySound(int index)
    {
        // Set the audio clip based on the index
        AudioSource.clip = Sounds[index].clip;
        // Play the audio clip
        AudioSource.Play();
    }

    // Function to play audio clips by name
    public void PlaySound(string name)
    {
        foreach (Sound sound in Sounds)
        {
            if (sound.name == name)
            {
                sound.source.Play();
            }
        }
    }
}