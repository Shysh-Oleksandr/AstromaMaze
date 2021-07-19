using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public SprayItem spray;
    public BootItem boot;
    public BirdsEyeViewItem birdsEyeView;
    public CompassItem compass;

    void Start()
    {
        for (int i = 0; i < MainMenuUI.Instance.priceTexts.Length; i++)
        {
            if (MainMenuUI.Instance.priceTexts[i].item.upgradingLevel == 0)
            {
                MainMenuUI.Instance.priceTexts[i].button.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.priceTexts[i].item.price1.ToString();
            }
            else if (MainMenuUI.Instance.priceTexts[i].item.upgradingLevel == 1)
            {
                MainMenuUI.Instance.priceTexts[i].button.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.priceTexts[i].item.price2.ToString();
            }
            else if (MainMenuUI.Instance.priceTexts[i].item.upgradingLevel == 2)
            {
                MainMenuUI.Instance.priceTexts[i].button.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.priceTexts[i].item.price3.ToString();
            }
            else if (MainMenuUI.Instance.priceTexts[i].item.upgradingLevel == 3)
            {
                MainMenuUI.Instance.priceTexts[i].button.GetComponentInChildren<TextMeshProUGUI>().text = MainMenuUI.Instance.priceTexts[i].item.price4.ToString();
            }
            else if (MainMenuUI.Instance.priceTexts[i].item.upgradingLevel == 4)
            {
                MainMenuUI.Instance.priceTexts[i].button.interactable = false;
                MainMenuUI.Instance.priceTexts[i].button.GetComponentInChildren<TextMeshProUGUI>().GetComponentInChildren<Image>().enabled = false;
                MainMenuUI.Instance.priceTexts[i].button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                MainMenuUI.Instance.priceTexts[i].maxLevelText.enabled = true;
            }
        }

        for (int i = 0; i < MainMenuUI.Instance.levelTexts.Length; i++)
        {
            MainMenuUI.Instance.levelTexts[i].text.text = MainMenuUI.Instance.levelTexts[i].item.upgradingLevel.ToString() + "/4";
        }
    }
}
