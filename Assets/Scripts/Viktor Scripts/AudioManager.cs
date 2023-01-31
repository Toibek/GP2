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
    public static AudioManager instance;

    // An array to store audio clips
    public AudioClip[] sounds;

    // An array of names to match the audio clips
    public string[] clipNames;

    // The AudioSource component used to play audio clips
    public AudioSource audioSource;

    private void Awake()
    {
        // Ensure that there is only one instance of the AudioManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to play audio clips
    public void PlaySound(int index)
    {
        // Set the audio clip based on the index
        audioSource.clip = sounds[index];
        // Play the audio clip
        audioSource.Play();
    }
}
