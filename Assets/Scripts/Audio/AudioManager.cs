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

    private FMOD.Studio.Bus AmbienceBus;
    private FMOD.Studio.Bus EnvironmentFXBus;
    private FMOD.Studio.Bus FootStepsBus;
    private FMOD.Studio.Bus UIBus;

    private StudioEventEmitter _ambientEmitter;
    // An array to store audio clips
    public SoundDatabase Sounds;

     // if (AudioManager.Instance) AudioManager.Instance.SetVolume(volumeSlider.value, Sound.Type.Ambient);

    private void Awake()
    {
        // Ensure that there is only one instance of the AudioManager
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

        AmbienceBus = RuntimeManager.GetBus("bus:/Ambience");
        EnvironmentFXBus = RuntimeManager.GetBus("bus:/EnvironmentFX");
        FootStepsBus = RuntimeManager.GetBus("bus:/Footsteps");
        UIBus = RuntimeManager.GetBus("bus:/UI");

        if (File.Exists(Application.persistentDataPath + "/"  + PlayerPrefsVariables.AmbientVolume))
        {
            float newVol = PlayerPrefs.GetFloat(PlayerPrefsVariables.AmbientVolume);
            AmbienceBus.setVolume(newVol);
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

    public void SetVolume(float newVolume,Sound.Type type)
    {
        switch (type)
        {
            case Sound.Type.Ambient:
                PlayerPrefs.SetFloat(PlayerPrefsVariables.AmbientVolume, newVolume);
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
            case Sound.Type.Unassigned:
                break;
        }
    }

    [ContextMenu("TestSound")]
    private void PlayTesting()
    {
        if (!Application.isPlaying) return;
        PlayOneShotSound(Sound.Names.SFX_Druids_DruidJump);
    }

    public static void S_PlayAmbientSound(Sound.Names name)
    {
        if (Instance == null) return;
        Debug.Log(((Sound.Names)name).ToString());

        Instance.PlayAmbientSound(name);
    }

    private void PlayAmbientSound(Sound.Names name)
    {
        Sound ambientSound = Sounds.GetSound(name);
        if (_ambientEmitter)
        {
            _ambientEmitter.Stop();
            _ambientEmitter.gameObject.name = "Stoping Ambient sound";
            StartCoroutine("StopAmbient", _ambientEmitter.gameObject);
        }

        GameObject _obj = new GameObject();
        _obj.transform.parent = transform;
        _obj.name = "Playing Ambient: " + ambientSound.name.ToString();
        _ambientEmitter = _obj.AddComponent<StudioEventEmitter>();

        _ambientEmitter.EventReference = ambientSound.eventRef;

        _ambientEmitter.Play();
    }

    private IEnumerator StopAmbient(GameObject obj)
    {
        float deleteAfter = 10f;

        while (deleteAfter > 0)
        {
            deleteAfter -= Time.deltaTime;
            yield return 0;
        }
        Destroy(obj);
        yield return null;
    }

    public static void S_PlayOneShotSound(Sound.Names name)
    {
        if (Instance == null) return;

        Instance.PlayOneShotSound(name);
    }
    public static void S_PlayOneShotSound(int name)
    {
        if (Instance == null) return;
        Debug.Log(((Sound.Names)name).ToString());

        Instance.PlayOneShotSound((Sound.Names) name);
    }

    /// <summary>
    /// Plays sound if sound could be found in sound Database
    /// </summary>
    /// <param name="name">exakt name</param>
    public void PlayOneShotSound(string name)
    {
        Sound sound = Sounds.GetSound(name);
        if (sound == null) return;

        var instanceOfSound = RuntimeManager.CreateInstance(sound.eventRef);
        RuntimeManager.AttachInstanceToGameObject(instanceOfSound, Camera.main.transform);
        instanceOfSound.start();
    }

    /// <summary>
    /// Plays sound if sound could be found in sound Database
    /// </summary>
    /// <param name="name">Sound.Names.ExampleName</param>
    public void PlayOneShotSound(Sound.Names name)
    {
        Sound sound = Sounds.GetSound(name);
        if (sound == null) return;

        var instanceOfSound = RuntimeManager.CreateInstance(sound.eventRef);
        RuntimeManager.AttachInstanceToGameObject(instanceOfSound, Camera.main.transform);
        instanceOfSound.start();
    }

    /// <summary>
    /// Plays oneShotAttached if sound could be found
    /// </summary>
    /// <param name="name">Name of sound to play</param>
    /// <param name="target">Gameobject Target that sound will follow</param>
    
    public void PlaySound(Sound.Names name, GameObject target)
    {
        Sound sound = Sounds.GetSound(name);
        if (sound == null) return;

        RuntimeManager.PlayOneShotAttached(sound.eventRef,target);

    }

    /// <summary>
    /// Plays oneShot if sound could be found
    /// </summary>
    /// <param name="name">Name of sound to play</param>
    /// <param name="target">sound origin</param>

    public void PlaySound(Sound.Names name, Vector3 target)
    {
        Sound sound = Sounds.GetSound(name);
        if (sound == null) return;

        RuntimeManager.PlayOneShot(sound.eventRef, target);

    }
}