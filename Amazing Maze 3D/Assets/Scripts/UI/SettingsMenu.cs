using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public LocalizationSetter localizationSetter;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown difficultyDropdown;
    public Slider qualitySlider, volumeSlider;
    public Toggle fullScreenToggle;
    public int quality = 1, startResolutionIndex, difficultyIndex = 1;
    public float startVolume = 1;
    public bool isFullScreen = true;

    Resolution[] resolutions;

    private void Start()
    {
        SetResolutionDropdownValues();

        fullScreenToggle.isOn = isFullScreen;
        qualitySlider.value = quality;
        difficultyDropdown.value = difficultyIndex;

        SetFullscreen(isFullScreen);
        SetQuality(quality);
        SetDifficulties(difficultyIndex);
    }

    public void SetDifficulties(int index)
    {
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
        startResolutionIndex = resolutionIndex;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        startVolume = volume;

        audioMixer.SetFloat("volume", Mathf.Log10(startVolume) * 20);
    }

    public void SetQuality(float qualityIndex)
    {
        quality = (int)qualityIndex;
        QualitySettings.SetQualityLevel((int)qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        isFullScreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.Play("Click");
    }

    public void LoadSettingsData()
    {
        SettingsData data = SaveSystem.LoadSettings();

        while(data == null)
        {
            isFullScreen = true;
            startVolume = 1;

            SaveSystem.SaveSettings(this);
            data = SaveSystem.LoadSettings();
        }
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
        SaveSystem.SaveLanguage(localizationSetter);
    }
}
