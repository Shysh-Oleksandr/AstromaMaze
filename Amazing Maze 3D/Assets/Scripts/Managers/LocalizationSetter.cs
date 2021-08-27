using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSetter : MonoBehaviour
{
    // 0 - English, 1 - Russian, 2 - Spanish, 3 - Ukrainian.
    public int languageIndex;

    public TMP_Dropdown languageDropdown;

    private void Awake()
    {
        LoadLanguageData();
        languageDropdown.value = languageIndex;
        StartCoroutine(SetLanguage(languageIndex));
    }

    IEnumerator Start()
    {
        // Wait for the localization system to initialize, loading Locales, preloading etc.
        yield return LocalizationSettings.InitializationOperation;

        // Generate list of available Locales
        var options = new List<TMP_Dropdown.OptionData>();
        int selected = 0;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
                selected = i;
            options.Add(new TMP_Dropdown.OptionData(locale.name));
        }
        languageDropdown.options = options;

        languageDropdown.value = languageIndex;
        languageDropdown.onValueChanged.AddListener(LocaleSelected);
    }

    public void LocaleSelected(int index)
    {
        AudioManager.Instance.Play("Click");
        languageIndex = index;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }

    public IEnumerator SetLanguage(int i)
    {
        // Wait for the localization system to initialize, loading Locales, preloading, etc.
        yield return LocalizationSettings.InitializationOperation;

        // This part changes the language
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
    }

    private void LoadLanguageData()
    {
        LanguageData data = SaveSystem.LoadLanguage();

        languageIndex = data.languageIndex;
    }

    /*private void OnDestroy()
    {
        SaveSystem.SaveLanguage(this);
    }*/

    private void OnApplicationQuit()
    {
        SaveSystem.SaveLanguage(this);
    }
}
