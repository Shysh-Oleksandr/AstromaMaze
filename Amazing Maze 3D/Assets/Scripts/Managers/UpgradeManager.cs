using System.Text.RegularExpressions;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public SprayItem spray;
    public BootItem boot;
    public BirdsEyeViewItem birdsEyeView;
    public CompassItem compass;

    public delegate void OnCoinChanged();

    public event OnCoinChanged OnCoinChangedEvent;

    void Start()
    {
        DefineItemPrice();
        DefineItemLevel();
        DefineItemStats();
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
                    GameManager.Instance.totalCoins -= itemPrice;
                    OnCoinChangedEvent?.Invoke();
                    UpdateItemLevel(MainMenuUI.Instance.upgradeElements[i].item);
                    DefineItemLevel();
                    DefineItemStats();
                    DefineItemPrice();
                }
                else
                {
                    Tweening.Instance.TweenVertically(MainMenuUI.Instance.noMoneyText.gameObject, 0.6f, 0, true, false);
                }
            }
        }
    }

    private void UpdateItemLevel(Item item)
    {
        item.upgradingLevel++;
    }

    private int GetItemPrice(Item item)
    {
        int price = 0;
        if (item.upgradingLevel == 0)
        {
            price = item.price1;
        }
        else if(item.upgradingLevel == 1)
        {
            price = item.price2;
        }
        else if(item.upgradingLevel == 2)
        {
            price = item.price3;
        }
        else if(item.upgradingLevel == 3)
        {
            price = item.price4;
        }

        return price;
    }

    private void DefineItemLevel()
    {
        for (int i = 0; i < MainMenuUI.Instance.upgradeElements.Length; i++)
        {
            MainMenuUI.Instance.upgradeElements[i].levelText.text = MainMenuUI.Instance.upgradeElements[i].item.upgradingLevel.ToString() + "/4";
        }
    }

    private void DefineItemStats()
    {
        //300/450/700/1000
        //<color=#E9BD31><b>300</b></color>

        for (int i = 0; i < MainMenuUI.Instance.upgradeElements.Length; i++)
        {
            if(MainMenuUI.Instance.upgradeElements[i].item == compass)
            {
                // Do special for compass
            }
            else
            {
                string pattern;
                pattern = @"\/?\w+";

                for (int stat = 0; stat < MainMenuUI.Instance.upgradeElements[i].statsTexts.Length; stat++)
                {
                    string statText = MainMenuUI.Instance.upgradeElements[i].statsTexts[stat].text;
                    statText = statText.Replace("<color=#E9BD31><b>", "");
                    statText = statText.Replace("</b></color>", "");
                    print("Stat text: " + statText);
                    Regex rg = new Regex(pattern);
                    MatchCollection matchedStats = rg.Matches(statText);
                    print("Matches count: " + matchedStats.Count);
                    var matchesArray = (from Match match in matchedStats select match.Value).ToArray();
                    for (int count = 0; count < matchesArray.Length; count++)
                    {
                        if(count < MainMenuUI.Instance.upgradeElements[i].item.upgradingLevel)
                        {
                            matchesArray[count] = matchesArray[count].Insert(0, "<color=#E9BD31><b>");
                            matchesArray[count] = matchesArray[count].Insert(matchesArray[count].Length, "</b></color>");
                            print("New match: " + matchesArray[count]);
                        }
                    }
                    print("Before join: " + MainMenuUI.Instance.upgradeElements[i].statsTexts[stat].text);
                    MainMenuUI.Instance.upgradeElements[i].statsTexts[stat].text = string.Join("", matchesArray);
                    print("After join: " + MainMenuUI.Instance.upgradeElements[i].statsTexts[stat].text);

                }
            }
        }


    }

    private void DefineItemPrice()
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
}
