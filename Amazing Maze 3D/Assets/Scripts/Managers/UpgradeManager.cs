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

    private void DefineItemPrice()
    {
        for (int i = 0; i < MainMenuUI.Instance.upgradeElements.Length; i++)
        {
            if (MainMenuUI.Instance.upgradeElements[i].item.upgradingLevel == 0)
            {
                MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.upgradeElements[i].item.price1.ToString();
            }
            else if (MainMenuUI.Instance.upgradeElements[i].item.upgradingLevel == 1)
            {
                MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.upgradeElements[i].item.price2.ToString();
            }
            else if (MainMenuUI.Instance.upgradeElements[i].item.upgradingLevel == 2)
            {
                MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.upgradeElements[i].item.price3.ToString();
            }
            else if (MainMenuUI.Instance.upgradeElements[i].item.upgradingLevel == 3)
            {
                MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.upgradeElements[i].item.price4.ToString();
            }
            else if (MainMenuUI.Instance.upgradeElements[i].item.upgradingLevel == 4)
            {
                MainMenuUI.Instance.upgradeElements[i].upgradeButton.interactable = false;
                MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().GetComponentInChildren<Image>().enabled = false;
                MainMenuUI.Instance.upgradeElements[i].upgradeButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                MainMenuUI.Instance.upgradeElements[i].maxLevelText.enabled = true;
            }
        }
    }
}
