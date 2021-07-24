using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown difficultyDropdown;
    public Slider qualitySlider, volumeSlider;
    public Toggle fullScreenToggle;
    public int quality = 1, startResolutionIndex, difficultyIndex = 1;
    public float startVolume = 1f;
    public bool isFullScreen;

    Resolution[] resolutions;

    private void Start()
    {
        LoadSettingsData();
        SetResolutionDropdownValues();

        SetResolution(startResolutionIndex); // Maybe not the best idea?
        SetFullscreen(isFullScreen);
        SetVolume(startVolume);
        SetQuality(quality);
        SetDifficulties(difficultyIndex);

        fullScreenToggle.isOn = isFullScreen;
        volumeSlider.value = startVolume;
        qualitySlider.value = quality;
        difficultyDropdown.value = difficultyIndex;
    }

    public void SetDifficulties(int index)
    {
        AudioManager.Instance.Play("Click");
        difficultyIndex = index;
        GameManager.Instance.difficultyCoefficient = index switch
        {
            // Easy
            0 => 1.5f,
            // Medium
            1 => 1f,
            // Hard
            2 => 0.75f,
            // Expert
            3 => 0.5f,
            // Default
            _ => 1f,
        };
    }

    private void SetResolutionDropdownValues()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        AudioManager.Instance.Play("Click");
        startResolutionIndex = resolutionIndex;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        startVolume = Mathf.Log10(volume) * 20;
        print(startVolume);
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(float qualityIndex)
    {
        AudioManager.Instance.Play("Click");
        quality = (int)qualityIndex;
        QualitySettings.SetQualityLevel((int)qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        AudioManager.Instance.Play("Click");
        isFullScreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    private void LoadSettingsData()
    {
        SettingsData data = SaveSystem.LoadSettings();

        quality = data.qualityLevel;
        startVolume = data.volume;
        isFullScreen = data.fullScreen;
        startResolutionIndex = data.resolutionIndex;
        difficultyIndex = data.difficultyIndex;
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveSettings(this);
    }

    private void OnDisable()
    {
        SaveSystem.SaveSettings(this);
    }
}
