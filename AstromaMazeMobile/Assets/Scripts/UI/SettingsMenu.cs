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
    public TMP_Dropdown difficultyDropdown;
    public Slider volumeSlider;
    public int difficultyIndex = 1;
    public float startVolume = 1;
    public float cameraSensitivity;

    private void Start()
    {
        difficultyDropdown.value = difficultyIndex;
        cameraSensitivity = GameManager.Instance.cameraSensitivity;
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

    public void SetCameraSensitivity(float sensitivity)
    {
        cameraSensitivity = sensitivity;

        GameManager.Instance.cameraSensitivity = sensitivity;
    }

    public void SetVolume(float volume)
    {
        startVolume = volume;

        audioMixer.SetFloat("volume", Mathf.Log10(startVolume) * 20);
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
            startVolume = 1;
            cameraSensitivity = 8;

            SaveSystem.SaveSettings(this);
            data = SaveSystem.LoadSettings();
        }
        startVolume = data.volume;
        difficultyIndex = data.difficultyIndex;
        GameManager.Instance.cameraSensitivity = data.cameraSensitivity;
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
