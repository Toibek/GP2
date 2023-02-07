using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// This script controls the volume of the audio in the scene and saves the value using PlayerPrefs.
/// </summary>

public class VolumeController : MonoBehaviour
{

    [SerializeField]
    Slider volumeSlider;

    private void Start()
    {
        // Check if PlayerPrefs has a key called "musicVolume"
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            // If not, set the default value for "musicVolume" to 1
            PlayerPrefs.SetFloat("musicVolume", 1);
            // Load the saved volume
            Load();
        }
        // If the key exists, directly load the saved volume
        else
        {
            Load();
        }
    }

    // Method to change the volume based on the value of the volumeSlider
    public void ChangeVolume()
    {
        // Set the volume of the audio listener to the value of the volumeSlider
        AudioListener.volume = volumeSlider.value;
        // Save the new volume value
        Save();
    }

    // Method to load the saved volume
    private void Load()
    {
        // Set the value of the volumeSlider to the saved "musicVolume" value
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    // Method to save the volume
    private void Save()
    {
        // Save the value of the volumeSlider to the "musicVolume" key in PlayerPrefs
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
