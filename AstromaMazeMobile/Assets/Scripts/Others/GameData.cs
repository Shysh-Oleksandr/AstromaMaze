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
    public int slowTimeLevel;
    public int bootsItemLevel;
    public int birdsItemLevel;
    public int compassItemLevel;

    public ItemData(UpgradeManager upgradeManager)
    {
        slowTimeLevel = upgradeManager.slowTime.upgradingLevel;
        bootsItemLevel = upgradeManager.boots.upgradingLevel;
        birdsItemLevel = upgradeManager.birdsEyeView.upgradingLevel;
        compassItemLevel = upgradeManager.compass.upgradingLevel;
    }
}

[System.Serializable]
public class SettingsData
{
    public float volume;
    public int difficultyIndex;
    public float cameraSensitivity;

    public SettingsData(SettingsMenu settingsMenu)
    {
        volume = settingsMenu.startVolume;
        difficultyIndex = settingsMenu.difficultyIndex;
        cameraSensitivity = settingsMenu.cameraSensitivity;
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


