using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JT_MenuManager : MonoBehaviour
{
    [Header("Canvas Parent")]
    [SerializeField] private GameObject gameSettings;
    [SerializeField] private GameObject audio;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject accessibility;
    [Space]
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject Blur;

    private void Start()
    {
        gameSettings.SetActive(true);
        GameManager.Instance.OnPause += Pause;
        GameManager.Instance.OnResume += Resume;
    }

    void Pause()
    {
        PauseMenu.SetActive(true);
        Blur.SetActive(true);
    }
    void Resume()
    {
        PauseMenu.SetActive(false);
        Blur.SetActive(false);

    }
    public void GameSettings()
    {
        gameSettings.SetActive(true);
        audio.SetActive(false);
        controls.SetActive(false);
        accessibility.SetActive(false);
    }

    public void Audio()
    {
        gameSettings.SetActive(false);
        audio.SetActive(true);
        controls.SetActive(false);
        accessibility.SetActive(false);
    }

    public void Controls()
    {
        gameSettings.SetActive(false);
        audio.SetActive(false);
        controls.SetActive(true);
        accessibility.SetActive(false);
    }

    public void Accessibility()
    {
        gameSettings.SetActive(false);
        audio.SetActive(false);
        controls.SetActive(false);
        accessibility.SetActive(true);
    }
}
