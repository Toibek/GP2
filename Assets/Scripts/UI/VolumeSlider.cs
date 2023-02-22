using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Sound.Type soundType;

    private void Start()
    {
        _slider.value = AudioManager.Instance.GetVolume(soundType);
    }

    public void UpdateSlider()
    {
        float volume = _slider.value;

        AudioManager.Instance.SetVolume(volume, soundType);
    }
}
