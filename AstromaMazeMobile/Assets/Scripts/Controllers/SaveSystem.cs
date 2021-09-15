using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGame(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetPath("/gameMobile.dat");
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            GameData data = new GameData(gameManager);

            formatter.Serialize(stream, data);
        }
    }

    public static GameData LoadGame()
    {
        string path = GetPath("/gameMobile.dat");

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return formatter.Deserialize(stream) as GameData;
            }
        }
        else
        {
            // Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }
    
    public static void SaveItems(UpgradeManager upgradeManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetPath("/itemsMobile.dat");
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            ItemData data = new ItemData(upgradeManager);

            formatter.Serialize(stream, data);
        }
    }

    public static ItemData LoadItems()
    {
        string path = GetPath("/itemsMobile.dat");

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return formatter.Deserialize(stream) as ItemData;
            }
        }
        else
        {
            // Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveSettings(SettingsMenu settingsMenu)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetPath("/settingsMobile.dat");
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            SettingsData data = new SettingsData(settingsMenu);

            formatter.Serialize(stream, data);
        }
    }

    public static SettingsData LoadSettings()
    {
        string path = GetPath("/settingsMobile.dat");

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return formatter.Deserialize(stream) as SettingsData;
            }
        }
        else
        {
            // Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }
    
    public static void SaveLanguage(LocalizationSetter localization)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetPath("/languageMobile.dat");
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            LanguageData data = new LanguageData(localization);

            formatter.Serialize(stream, data);
        }
    }

    public static LanguageData LoadLanguage()
    {
        string path = GetPath("/languageMobile.dat");

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return formatter.Deserialize(stream) as LanguageData;
            }
        }
        else
        {
            //Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }

    private static string GetPath(string path)
    {
        return Path.Combine(Application.persistentDataPath, path);
    }
}
