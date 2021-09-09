[System.Serializable]
public class GameData
{
    public int totalCoins;
    public int maxLevelReached;
    public float difficultyCoefficient;

    public GameData(GameManager gameManager)
    {
        totalCoins = gameManager.totalCoins;
        maxLevelReached = gameManager.maxLevelReached;
        difficultyCoefficient = gameManager.difficultyCoefficient;
    }
}

[System.Serializable]
public class ItemData
{
    public int sprayItemLevel;
    public int bootsItemLevel;
    public int birdsItemLevel;
    public int compassItemLevel;

    public ItemData(UpgradeManager upgradeManager)
    {
        sprayItemLevel = upgradeManager.spray.upgradingLevel;
        bootsItemLevel = upgradeManager.boots.upgradingLevel;
        birdsItemLevel = upgradeManager.birdsEyeView.upgradingLevel;
        compassItemLevel = upgradeManager.compass.upgradingLevel;
    }
}

[System.Serializable]
public class SettingsData
{
    public int qualityLevel; // 0 - low, 1 - medium, 2 - high, 3 - ultra
    public float volume;
    public bool fullScreen;
    public int resolutionIndex;
    public int difficultyIndex;

    public SettingsData(SettingsMenu settingsMenu)
    {
        qualityLevel = settingsMenu.quality;
        volume = settingsMenu.startVolume;
        fullScreen = settingsMenu.isFullScreen;
        resolutionIndex = settingsMenu.startResolutionIndex;
        difficultyIndex = settingsMenu.difficultyIndex;
    }
}

[System.Serializable]
public class LanguageData
{
    public int languageIndex; // 0 - Eng, 1 - Ru, 2 - Esp, 3 - Ukraine.

    public LanguageData(LocalizationSetter localization)
    {
        languageIndex = localization.languageIndex;
    }
}


