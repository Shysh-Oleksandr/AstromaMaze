using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGame(GameManager gameManager)
    {
        string path = GetPath("gameMobile.sg");

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Create);
            GameData data = new GameData(gameManager);
            bf.Serialize(file, data);
            file.Close();
            // Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }

    public static GameData LoadGame()
    {
        string path = GetPath("gameMobile.sg");

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
        string path = GetPath("itemsMobile.sg");

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Create);
            ItemData data = new ItemData(upgradeManager);
            bf.Serialize(file, data);
            file.Close();
            // Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }

    public static ItemData LoadItems()
    {
        string path = GetPath("itemsMobile.sg");

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
        string path = GetPath("settingsMobile.sg");

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Create);
            SettingsData data = new SettingsData(settingsMenu);
            bf.Serialize(file, data);
            file.Close();
            // Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }

    }

    public static SettingsData LoadSettings()
    {
        string path = GetPath("settingsMobile.sg");

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
        string path = GetPath("languageMobile.sg");

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Create);
            LanguageData data = new LanguageData(localization);
            bf.Serialize(file, data);
            file.Close();
            // Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }

    public static LanguageData LoadLanguage()
    {
        string path = GetPath("languageMobile.sg");

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
            return null;
        }
    }

    private static string GetPath(string path)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "data");
        return Path.Combine(filePath, path);
    }
}
