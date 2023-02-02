using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)] // Adds a slider to the interface in unity that has values between 0 - 1 
    public float volume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}