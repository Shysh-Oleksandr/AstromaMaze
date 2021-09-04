using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


[Serializable]
public struct UpgradeElements
{
    public Item item;
    public Button upgradeButton;
    public TextMeshProUGUI maxLevelText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI[] statsTexts;
    public ItemStats[] itemStats;
}

[Serializable]
public struct CompassStats
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI statText;
}

[Serializable]
public class ItemStats
{
    public string statName;
    public int[] stat;
}

[Serializable]
public class TabsContents
{
    public GameObject tab;
    public GameObject[] content;
}

public class MainMenuUI : MonoBehaviour
{
    private const float menuY = 132f, menuPosZ = -53.10346f;

    #region Variables declaration
    [Header("Menus")]
    public Transform optionsMenu;
    public Transform shopMenu;

    [Header("Bg")]
    public Image[] starsBgImages;

    [Header("Tab Elements")]
    public GameObject[] mainMenuElements;

    [Header("Tween variables")]
    [SerializeField] private float screenCenterX;
    [SerializeField] private float tweenDuration = 0.4f;
    [SerializeField] private float tweenDelay = 0.2f;

    [Header("Dictionaries")]
    public UpgradeElements[] upgradeElements;
    public CompassStats[] compassStats;
    public TabsContents[] tabsContents;
    [Header("Texts")]
    public TextMeshProUGUI maxLevelUpgradedText;
    public TextMeshProUGUI totalCoinsText;
    public TextMeshProUGUI noMoneyText;
    [Header("References")]
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private SettingsMenu settingsMenu;
    public AudioMixer mainMixer;
    public Button backButtonOption, backButtonShop;
    public Slider volumeSlider;
    private Vector3 optionsMenuStartPos, shopMenuStartPos;

    [Header("Tabs arrays")]
    [SerializeField] private GameObject[] optionsTabs;
    [SerializeField] private GameObject[] shopsTabs;

    #endregion

    #region Singleton 
    private static MainMenuUI instance;

    public static MainMenuUI Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    private void OnEnable()
    {
        upgradeManager.OnCoinChangedEvent += ChangeCoinsText;
    }
    private void OnDisable()
    {
        upgradeManager.OnCoinChangedEvent -= ChangeCoinsText;
    }

    private void Start()
    {
        settingsMenu.LoadSettingsData();
        volumeSlider.value = settingsMenu.startVolume;
        settingsMenu.fullScreenToggle.isOn = settingsMenu.isFullScreen;
        settingsMenu.qualitySlider.value = settingsMenu.quality;
        settingsMenu.difficultyDropdown.value = settingsMenu.difficultyIndex;
        settingsMenu.SetFullscreen(settingsMenu.isFullScreen);
        settingsMenu.SetQuality(settingsMenu.quality);
        settingsMenu.SetDifficulties(settingsMenu.difficultyIndex);

        upgradeManager.LoadItemsData();
        upgradeManager.UpdateItemStats();

        optionsMenuStartPos = new Vector3(Screen.width, menuY, menuPosZ);
        shopMenuStartPos = new Vector3(-Screen.width, menuY, menuPosZ);

        Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, 0.35f, true);

        totalCoinsText.text = GameManager.Instance.totalCoins.ToString();

        SceneChanger.Instance.ChangeStarsBgColor();


        foreach (var tabsContent in tabsContents)
        {
            tabsContent.tab.GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.Play("Click"));
            tabsContent.tab.GetComponent<Button>().onClick.AddListener(() => TweenTab(tabsContent.tab, tabsContent.content, tweenDuration, tweenDelay));
        }
    }

    public void TweenTab(GameObject tab, GameObject[] elements, float duration, float delay)
    {
        if (!tab.GetComponent<DevionGames.UIWidgets.Tab>().isOn)
        {
            tab.GetComponent<DevionGames.UIWidgets.Tab>().isOn = true;
            Tweening.Instance.TweenArray(elements, duration, delay, false);
        }
    }

    private IEnumerator BackButtonTweenCoroutine(bool isOptionsMenu)
    {
        while (Tweening.Instance.isTweening)
        {
            yield return null;
        }
        if (isOptionsMenu)
        {
            DisableOptionsMenu();
        }
        else
        {
            DisableShopMenu();
        }
    }

    public void BackButtonTween(bool isOptionsMenu)
    {
        if (isOptionsMenu)
        {
            backButtonOption.interactable = false;

            // Setting interactable to false for all options tabs.
            for (int i = 0; i < 4; i++)
            {
                tabsContents[i].tab.GetComponent<Button>().interactable = false;
            }

        }
        else
        {
            backButtonShop.interactable = false;
            // Setting interactable to false for all shops tabs.
            for (int i = 4; i < 8; i++)
            {
                tabsContents[i].tab.GetComponent<Button>().interactable = false;
            }
        }
        StartCoroutine(BackButtonTweenCoroutine(isOptionsMenu));
    }

    private void CallActiveTabEvent(GameObject tab)
    {
        tab.GetComponent<DevionGames.UIWidgets.Tab>().isOn = false;
        tab.GetComponent<Button>().onClick.Invoke();
    }

    public void EnableOptionsMenu()
    {
        AudioManager.Instance.Play("Swap");
        SceneChanger.Instance.SetRandomHue(starsBgImages[1], true);

        optionsMenu.gameObject.SetActive(true);
        backButtonOption.interactable = true;
        // Setting interactable to true for all options tabs.
        for (int i = 0; i < 4; i++)
        {
            tabsContents[i].tab.GetComponent<Button>().interactable = true;
        }

        GameObject activeTab = null;

        foreach (GameObject tab in optionsTabs)
        {
            if (tab.GetComponent<DevionGames.UIWidgets.Tab>().isOn)
            {
                activeTab = tab;
                foreach (var tabContent in tabsContents)
                {
                    if (tabContent.tab == tab)
                    {
                        foreach (var content in tabContent.content)
                        {
                            content.SetActive(false);
                        }
                    }
                }
            }
        }

        optionsMenu.localPosition = optionsMenuStartPos;
        optionsMenu.LeanMoveLocalX(screenCenterX, tweenDuration).setEaseOutExpo().setOnComplete(() => CallActiveTabEvent(activeTab));
    }

    public void EnableShopMenu()
    {
        AudioManager.Instance.Play("Swap");
        SceneChanger.Instance.SetRandomHue(starsBgImages[2], true);

        shopMenu.gameObject.SetActive(true);
        backButtonShop.interactable = true;
        // Setting interactable to true for all shops tabs.
        for (int i = 4; i < 8; i++)
        {
            tabsContents[i].tab.GetComponent<Button>().interactable = true;
        }

        GameObject activeTab = null;

        foreach (GameObject tab in shopsTabs)
        {
            if (tab.GetComponent<DevionGames.UIWidgets.Tab>().isOn)
            {
                activeTab = tab;
                foreach (var tabContent in tabsContents)
                {
                    if (tabContent.tab == tab)
                    {
                        foreach (var content in tabContent.content)
                        {
                            content.SetActive(false);
                        }
                    }
                }
            }
        }

        shopMenu.localPosition = shopMenuStartPos;
        shopMenu.LeanMoveLocalX(screenCenterX, tweenDuration).setEaseOutExpo().setOnComplete(() => CallActiveTabEvent(activeTab));
    }

    private void DisableOptionsMenu()
    {
        AudioManager.Instance.Play("Swap");
        SceneChanger.Instance.SetRandomHue(starsBgImages[0], true);

        for (int i = 0; i < mainMenuElements.Length; i++)
        {
            mainMenuElements[i].SetActive(false);
        }
        optionsMenu.LeanMoveLocalX(optionsMenuStartPos.x, tweenDuration).setEaseInExpo()
        .setOnComplete(() =>
        {
            optionsMenu.gameObject.SetActive(false);
            Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, tweenDelay, false);
        });
    }

    private void DisableShopMenu()
    {
        AudioManager.Instance.Play("Swap");
        SceneChanger.Instance.SetRandomHue(starsBgImages[0], true);
        for (int i = 0; i < mainMenuElements.Length; i++)
        {
            mainMenuElements[i].SetActive(false);
        }
        shopMenu.LeanMoveLocalX(shopMenuStartPos.x, tweenDuration).setEaseInExpo()
        .setOnComplete(() =>
        {
            shopMenu.gameObject.SetActive(false);
            Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, tweenDelay, false);
        });
    }

    private void ChangeCoinsText()
    {
        totalCoinsText.text = GameManager.Instance.totalCoins.ToString();
    }
}
