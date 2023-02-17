using UnityEngine.Audio;
using System;
using System.IO;
using UnityEngine;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script creates a singleton instance of the "AudioManager" so that it can be accessed from anywhere in the scene, and plays audio clips based on their names.
/// </summary>

public class AudioManager : MonoBehaviour
{
    // Singleton instance of the AudioManager
    public static AudioManager Instance;

    private FMOD.Studio.Bus MasterBus;
    private FMOD.Studio.Bus AmbienceBus;
    private FMOD.Studio.Bus EnvironmentFXBus;
    private FMOD.Studio.Bus FootStepsBus;
    private FMOD.Studio.Bus UIBus;

    // An array to store audio clips
    public SoundDatabase Sounds;

    private void Awake()
    {
        // Ensure that there is only one instance of the AudioManager
        if (Instance == null)
        {
            Instance = this;
        }
        AmbienceBus = RuntimeManager.GetBus("bus:/Master/Ambience");
        EnvironmentFXBus = RuntimeManager.GetBus("bus:/Master/EnvironmentFX");
        FootStepsBus = RuntimeManager.GetBus("bus:/Master/Footsteps");
        UIBus = RuntimeManager.GetBus("bus:/Master/UI");
        MasterBus = RuntimeManager.GetBus("bus:/Master");

        if (File.Exists(Application.persistentDataPath + "/"  + PlayerPrefsVariables.MasterVolume))
        {
            float newVol = PlayerPrefs.GetFloat(PlayerPrefsVariables.MasterVolume);
            MasterBus.setVolume(newVol);
        }
        if (File.Exists(Application.persistentDataPath + "/" + PlayerPrefsVariables.EnviormentFXVolume))
        {
            float newVol = PlayerPrefs.GetFloat(PlayerPrefsVariables.EnviormentFXVolume);
            EnvironmentFXBus.setVolume(newVol);
        }
        if (File.Exists(Application.persistentDataPath + "/" + PlayerPrefsVariables.FootStepsVolume))
        {
            float newVol = PlayerPrefs.GetFloat(PlayerPrefsVariables.FootStepsVolume);
            FootStepsBus.setVolume(newVol);
        }
        if (File.Exists(Application.persistentDataPath + "/" + PlayerPrefsVariables.UIVolume))
        {
            float newVol = PlayerPrefs.GetFloat(PlayerPrefsVariables.UIVolume);
            UIBus.setVolume(newVol);
        }
    }

    private void Update()
    {
        
    }


    public void SetVolume(float newVolume,Sound.Type type)
    {
        switch (type)
        {
            case Sound.Type.Ambient:
                PlayerPrefs.SetFloat(PlayerPrefsVariables.MasterVolume, newVolume);
                AmbienceBus.setVolume(newVolume);
                break;
            case Sound.Type.EnviromentFX:
                PlayerPrefs.SetFloat(PlayerPrefsVariables.EnviormentFXVolume, newVolume);
                AmbienceBus.setVolume(newVolume);
                break;
            case Sound.Type.Footsteps:
                PlayerPrefs.SetFloat(PlayerPrefsVariables.FootStepsVolume, newVolume);
                AmbienceBus.setVolume(newVolume);
                break;
            case Sound.Type.UI:
                PlayerPrefs.SetFloat(PlayerPrefsVariables.UIVolume, newVolume);
                AmbienceBus.setVolume(newVolume);
                break;
            case Sound.Type.Unassigend:
                break;
        }
    }

    // Function to play audio clips by name
    public void PlaySound(Sound.Names name)
    {
        
    }
}