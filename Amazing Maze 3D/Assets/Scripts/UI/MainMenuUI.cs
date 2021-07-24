using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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

[System.Serializable]
public class ItemStats
{
    public string statName;
    public int[] stat;
}

public class MainMenuUI : MonoBehaviour
{
    private const float menuY = 132f;

    #region Variables declaration
    [Header("Menus")]
    public Transform optionsMenu;
    public Transform shopMenu;

    [Header("Tab Elements")]
    public GameObject[] mainMenuElements;
    [SerializeField] private GameObject[] videoElements;
    [SerializeField] private GameObject[] audioElements;
    [SerializeField] private GameObject[] gameElements;
    [SerializeField] private GameObject[] languageElements;
    [SerializeField] private GameObject[] sprayElements;
    [SerializeField] private GameObject[] bootsElements;
    [SerializeField] private GameObject[] birdsElements;
    [SerializeField] private GameObject[] compassElements;

    [Header("Tabs")]
    [SerializeField] private GameObject videoTab;
    [SerializeField] private GameObject audioTab;
    [SerializeField] private GameObject gameTab;
    [SerializeField] private GameObject languageTab;
    [SerializeField] private GameObject sprayTab;
    [SerializeField] private GameObject bootsTab;
    [SerializeField] private GameObject birdsTab;
    [SerializeField] private GameObject compassTab;

    [Header("Tween variables")]
    [SerializeField] private float screenCenterX;
    [SerializeField] private float tweenDuration = 0.4f;
    [SerializeField] private float tweenDelay = 0.2f;

    [Header("Upgrade dictionaries")]
    public UpgradeElements[] upgradeElements;
    public CompassStats[] compassStats;
    [Header("Texts")]
    public TextMeshProUGUI maxLevelUpgradedText;
    public TextMeshProUGUI totalCoinsText;
    public TextMeshProUGUI noMoneyText;
    [Header("References")]
    [SerializeField] private UpgradeManager upgradeManager;
    public Vector2 optionsMenuStartPos, shopMenuStartPos;
    public Button backButtonOption, backButtonShop;

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
        #region Tab click sounds
        videoTab.GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.Play("Click"));
        audioTab.GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.Play("Click"));
        gameTab.GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.Play("Click"));
        languageTab.GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.Play("Click"));
        sprayTab.GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.Play("Click"));
        bootsTab.GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.Play("Click"));
        birdsTab.GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.Play("Click"));
        compassTab.GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.Play("Click"));
        #endregion
        optionsMenuStartPos = new Vector2(Screen.width, menuY);
        shopMenuStartPos = new Vector2(-Screen.width, menuY);

        Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, 0.35f, true);

        totalCoinsText.text = GameManager.Instance.totalCoins.ToString();

        #region Adding listeners to the tabs.
        videoTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(videoTab, videoElements, tweenDuration, tweenDelay));
        audioTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(audioTab, audioElements, tweenDuration, tweenDelay));
        gameTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(gameTab, gameElements, tweenDuration, tweenDelay));
        languageTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(languageTab, languageElements, tweenDuration, tweenDelay));
        sprayTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(sprayTab, sprayElements, 0.15f, 0.2f));
        bootsTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(bootsTab, bootsElements, 0.15f, 0.2f));
        birdsTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(birdsTab, birdsElements, 0.15f, 0.2f));
        compassTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(compassTab, compassElements, 0.15f, 0.2f));
        #endregion
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
            videoTab.GetComponent<Button>().interactable = false;
            audioTab.GetComponent<Button>().interactable = false;
            gameTab.GetComponent<Button>().interactable = false;
            languageTab.GetComponent<Button>().interactable = false;
        }
        else
        {
            backButtonShop.interactable = false;
            sprayTab.GetComponent<Button>().interactable = false;
            bootsTab.GetComponent<Button>().interactable = false;
            birdsTab.GetComponent<Button>().interactable = false;
            compassTab.GetComponent<Button>().interactable = false;
        }
        StartCoroutine(BackButtonTweenCoroutine(isOptionsMenu));
    }

    public void EnableOptionsMenu()
    {
        AudioManager.Instance.Play("Swap");

        optionsMenu.gameObject.SetActive(true);
        backButtonOption.interactable = true;
        videoTab.GetComponent<Button>().interactable = true;
        audioTab.GetComponent<Button>().interactable = true;
        gameTab.GetComponent<Button>().interactable = true;
        languageTab.GetComponent<Button>().interactable = true;

        optionsMenu.localPosition = optionsMenuStartPos;
        optionsMenu.LeanMoveLocalX(screenCenterX, tweenDuration).setEaseOutExpo();
    }

    public void EnableShopMenu()
    {
        AudioManager.Instance.Play("Swap");

        shopMenu.gameObject.SetActive(true);
        backButtonShop.interactable = true;
        sprayTab.GetComponent<Button>().interactable = true;
        bootsTab.GetComponent<Button>().interactable = true;
        birdsTab.GetComponent<Button>().interactable = true;
        compassTab.GetComponent<Button>().interactable = true;

        shopMenu.localPosition = shopMenuStartPos;
        shopMenu.LeanMoveLocalX(screenCenterX, tweenDuration).setEaseOutExpo();
    }

    private void DisableOptionsMenu()
    {
        AudioManager.Instance.Play("Swap");
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
