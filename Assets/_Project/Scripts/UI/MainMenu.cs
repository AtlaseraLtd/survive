using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    [Header("Settings")]
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Make sure only main panel is visible on start
        ShowMain();

        // Load saved audio settings if they exist
        if (musicSlider) musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        if (sfxSlider) sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    // ── Navigation ──────────────────────────────────────

    public void StartGame()
    {
        GameManager.LoadSceneWithLoading("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit"); // Only visible in Editor
    }

    public void ShowMain()
    {
        SetPanels(main: true, settings: false, credits: false);
    }

    public void ShowSettings()
    {
        SetPanels(main: false, settings: true, credits: false);
    }

    public void ShowCredits()
    {
        SetPanels(main: false, settings: false, credits: true);
    }

    void SetPanels(bool main, bool settings, bool credits)
    {
        if (mainPanel) mainPanel.SetActive(main);
        if (settingsPanel) settingsPanel.SetActive(settings);
        if (creditsPanel) creditsPanel.SetActive(credits);
    }

    // ── Settings ─────────────────────────────────────────

    public void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        // TODO: hook into AudioManager
    }

    public void OnSFXVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        // TODO: hook into AudioManager
    }
}