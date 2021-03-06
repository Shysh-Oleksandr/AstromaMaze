using System.Text.RegularExpressions;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public SlowTimeItem slowTime;
    public BootItem boots;
    public BirdsEyeViewItem birdsEyeView;
    public CompassItem compass;

    public delegate void OnCoinChanged();

    public event OnCoinChanged OnCoinChangedEvent;
     
    void Start()
    {
        LoadItemsData();

        DefineItemsPrices();
        DefineItemsLevels();
        DefineItemsStats();
        UpdateItemStats();
    }

    public void SubtractCoins(Button button)
    {
        for (int i = 0; i < MainMenuUI.Instance.upgradeElements.Length; i++)
        {
            if (MainMenuUI.Instance.upgradeElements[i].upgradeButton == button)
            {
                int itemPrice = GetItemPrice(MainMenuUI.Instance.upgradeElements[i].item);
                if (GameManager.Instance.totalCoins >= itemPrice)
                {
                    AudioManager.Instance.Play("Buy");
                    GameManager.Instance.totalCoins -= itemPrice;
                    OnCoinChangedEvent?.Invoke();
                    UpdateItemLevel(MainMenuUI.Instance.upgradeElements[i].item);
                    DefineItemsLevels();
                    DefineItemsStats();
                    DefineItemsPrices();
                    UpdateItemStats();
                }
                else
                {
                    Tweening.Instance.TweenVertically(MainMenuUI.Instance.noMoneyText.gameObject, 0.6f, 0, true, false);
                }
            }
        }
    }

    public void UpdateItemStats()
    {
        slowTime.duration = (int)MainMenuUI.Instance.upgradeElements[0].itemStats[0].stat[slowTime.upgradingLevel - 1]; // Careful in inspector!!!!!!!!
        slowTime.slowCoefficient = MainMenuUI.Instance.upgradeElements[0].itemStats[1].stat[slowTime.upgradingLevel - 1];
        slowTime.cooldown = (int)MainMenuUI.Instance.upgradeElements[0].itemStats[2].stat[slowTime.upgradingLevel - 1];
        slowTime.speedUpCoefficient = MainMenuUI.Instance.upgradeElements[0].itemStats[3].stat[slowTime.upgradingLevel - 1];

        boots.speed = MainMenuUI.Instance.upgradeElements[1].itemStats[0].stat[boots.upgradingLevel - 1];
        boots.baseLifetime = MainMenuUI.Instance.upgradeElements[1].itemStats[1].stat[boots.upgradingLevel - 1];

        birdsEyeView.birdsEyeViewCooldown = MainMenuUI.Instance.upgradeElements[2].itemStats[0].stat[birdsEyeView.upgradingLevel - 1];
        birdsEyeView.coroutineDuration = MainMenuUI.Instance.upgradeElements[2].itemStats[1].stat[birdsEyeView.upgradingLevel - 1];
        birdsEyeView.maxHeight = MainMenuUI.Instance.upgradeElements[2].itemStats[2].stat[birdsEyeView.upgradingLevel - 1];
        birdsEyeView.maxHeightDuration = MainMenuUI.Instance.upgradeElements[2].itemStats[3].stat[birdsEyeView.upgradingLevel - 1];

        compass.isBought = (compass.upgradingLevel >= 1);
        compass.canRotateToNorth = (compass.upgradingLevel >= 2);
        if(compass.upgradingLevel == 2)
        {
            compass.compassCooldown = 30;
        }
        if(compass.upgradingLevel >= 3)
        {
            compass.compassCooldown = 0;
        }
        compass.canShowDistance = (compass.upgradingLevel == 4);
    }

    private void UpdateItemLevel(Item item)
    {
        item.upgradingLevel++;
    }


    private void DefineItemsLevels()
    {
        for (int i = 0; i < MainMenuUI.Instance.upgradeElements.Length; i++)
        {
            MainMenuUI.Instance.upgradeElements[i].levelText.text = MainMenuUI.Instance.upgradeElements[i].item.upgradingLevel.ToString() + "/4";
        }
    }

    private void DefineItemsStats()
    {
        for (int i = 0; i < MainMenuUI.Instance.upgradeElements.Length; i++)
        {
            if (MainMenuUI.Instance.upgradeElements[i].item == compass)
            {
                for (int level = 0; level < MainMenuUI.Instance.compassStats.Length; level++)
                {
                    if (level < MainMenuUI.Instance.upgradeElements[i].item.upgradingLevel)
                    {
                        MainMenuUI.Instance.compassStats[level].levelText.color = new Color32(233, 189, 49, 255);
                        MainMenuUI.Instance.compassStats[level].levelText.fontStyle = FontStyles.Bold;
                        MainMenuUI.Instance.compassStats[level].statText.color = new Color32(233, 189, 49, 255);
                        MainMenuUI.Instance.compassStats[level].statText.fontStyle = FontStyles.Bold;
                    }
                }
            }
            else
            {
                string pattern;
                pattern = @"(\/?)([0-9]*[.])?[0-9]+";

                for (int stat = 0; stat < MainMenuUI.Instance.upgradeElements[i].statsTexts.Length; stat++)
                {
                    string statText = MainMenuUI.Instance.upgradeElements[i].statsTexts[stat].text;
                    statText = statText.Replace("<color=#E9BD31><b>", "");
                    statText = statText.Replace("</b></color>", "");
                    Regex rg = new Regex(pattern);
                    MatchCollection matchedStats = rg.Matches(statText);
                    var matchesArray = (from Match match in matchedStats select match.Value).ToArray();
                    for (int count = 0; count < matchesArray.Length; count++)
                    {
                        if (count < MainMenuUI.Instance.upgradeElements[i].item.upgradingLevel)
                        {
                            matchesArray[count] = matchesArray[count].Insert(0, "<color=#E9BD31><b>");
                            matchesArray[count] = matchesArray[count].Insert(matchesArray[count].Length, "</b></color>");
                        }
                    }
                    MainMenuUI.Instance.upgradeElements[i].statsTexts[stat].text = string.Join("", matchesArray);
                }
            }
        }
    }

    private void DefineItemsPrices()
    {
        for (int i = 0; i < MainMenuUI.Instance.upgradeElements.Length; i++)
        {
            switch (MainMenuUI.Instance.upgradeElements[i].item.upgradingLevel)
            {
                case 0:
                    MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.upgradeElements[i].item.price1.ToString();
                    break;
                case 1:
                    MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.upgradeElements[i].item.price2.ToString();
                    break;
                case 2:
                    MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.upgradeElements[i].item.price3.ToString();
                    break;
                case 3:
                    MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.upgradeElements[i].item.price4.ToString();
                    break;
                case 4:
                    MainMenuUI.Instance.upgradeElements[i].upgradeButton.interactable = false;
                    MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().GetComponentInChildren<Image>().enabled = false;
                    MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                    MainMenuUI.Instance.upgradeElements[i].maxLevelText.enabled = true;
                    break;
            }
        }
    }
    private int GetItemPrice(Item item)
    {
        int price = 0;
        if (item.upgradingLevel == 0)
        {
            price = item.price1;
        }
        else if (item.upgradingLevel == 1)
        {
            price = item.price2;
        }
        else if (item.upgradingLevel == 2)
        {
            price = item.price3;
        }
        else if (item.upgradingLevel == 3)
        {
            price = item.price4;
        }

        return price;
    }

    public void LoadItemsData()
    {
        ItemData data = SaveSystem.LoadItems();

        while (data == null)
        {
            slowTime.upgradingLevel = 1;
            boots.upgradingLevel = 1;
            birdsEyeView.upgradingLevel = 1;
            compass.upgradingLevel = 1;

            SaveSystem.SaveItems(this);
            data = SaveSystem.LoadItems();
        }

        slowTime.upgradingLevel = data.slowTimeLevel;
        boots.upgradingLevel = data.bootsItemLevel;
        birdsEyeView.upgradingLevel = data.birdsItemLevel;
        compass.upgradingLevel = data.compassItemLevel;
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveItems(this);
    }

    private void OnDisable()
    {
        SaveSystem.SaveItems(this);
    }
}
