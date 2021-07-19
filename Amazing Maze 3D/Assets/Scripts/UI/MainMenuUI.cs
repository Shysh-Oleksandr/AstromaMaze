using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct UpgradeTexts
{
    public Item item;
    public TextMeshProUGUI text;
}

[Serializable]
public struct UpgradeButtons
{
    public Item item;
    public Button button;
    public TextMeshProUGUI maxLevelText;
}

public class MainMenuUI : MonoBehaviour
{
    private const float menuY = 132f;

    #region Variables declaration
    [Header("Menus")]
    [SerializeField] private Transform optionsMenu;
    [SerializeField] private Transform shopMenu;

    [Header("Tab Elements")]
    [SerializeField] private GameObject[] mainMenuElements;
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
    public UpgradeButtons[] priceTexts;
    public UpgradeTexts[] levelTexts;
    [Header("Texts")]
    public TextMeshProUGUI maxLevelUpgradedText;
    public TextMeshProUGUI totalCoinsText;
    public TextMeshProUGUI noMoneyText;
    [Header("References")]
    [SerializeField] private UpgradeManager upgradeManager;


    private int id;
    private bool isTweening;
    private Vector2 optionsMenuStartPos, shopMenuStartPos;
    private LTDescr d;

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
        optionsMenuStartPos = new Vector2(Screen.width, menuY);
        shopMenuStartPos = new Vector2(-Screen.width, menuY);

        Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, 0.35f, true);

        totalCoinsText.text = GameManager.Instance.totalCoins.ToString();

        #region Adding listeners to the tabs.
        videoTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(videoTab, videoElements, tweenDuration, tweenDelay));
        audioTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(audioTab, audioElements, tweenDuration, tweenDelay));
        gameTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(gameTab, gameElements, tweenDuration, tweenDelay));
        languageTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(languageTab, languageElements, tweenDuration, tweenDelay));
        sprayTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(sprayTab, sprayElements, 0.25f, 0.1f));
        bootsTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(bootsTab, bootsElements, 0.25f, 0.1f));
        birdsTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(birdsTab, birdsElements, 0.25f, 0.1f));
        compassTab.GetComponent<Button>().onClick.AddListener(() => TweenTab(compassTab, compassElements, 0.25f, 0.1f));
        #endregion
    }

    private void Update()
    {
        d = LeanTween.descr(id);

        if (d != null)
        {
            isTweening = true;
        }
        else
        {
            isTweening = false;
        }
    }

    private void TweenDiagonally(GameObject[] optionElements, float duration, float delay)
    {
        for (int i = 0; i < optionElements.Length; i++)
        {
            GameObject element = optionElements[i];
            Vector2 elementStartPos = element.transform.position;
            element.transform.localPosition = new Vector2(elementStartPos.x, -Screen.height);

            id = LeanTween.move(element, elementStartPos, duration).setEaseOutExpo().id;

            LTDescr descr = LeanTween.descr(id);
            descr.delay = delay * i;
        }
    }

    public void TweenTab(GameObject tab, GameObject[] elements, float duration, float delay)
    {
        if (!tab.GetComponent<DevionGames.UIWidgets.Tab>().isOn && !isTweening)
        {
            tab.GetComponent<DevionGames.UIWidgets.Tab>().isOn = true;
            TweenDiagonally(elements, duration, delay);
        }
    }

    public void EnableOptionsMenu()
    {
        optionsMenu.gameObject.SetActive(true);

        optionsMenu.localPosition = optionsMenuStartPos;
        optionsMenu.LeanMoveLocalX(screenCenterX, tweenDuration).setEaseOutExpo();
    }

    public void EnableShopMenu()
    {
        shopMenu.gameObject.SetActive(true);

        shopMenu.localPosition = shopMenuStartPos;
        shopMenu.LeanMoveLocalX(screenCenterX, tweenDuration).setEaseOutExpo();
    }

    public void DisableOptionsMenu()
    {
        optionsMenu.LeanMoveLocalX(optionsMenuStartPos.x, tweenDuration).setEaseInExpo().setOnComplete(() => optionsMenu.gameObject.SetActive(false));
        Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, tweenDelay, true);
    }

    public void DisableShopMenu()
    {
        shopMenu.LeanMoveLocalX(shopMenuStartPos.x, tweenDuration).setEaseInExpo().setOnComplete(() => shopMenu.gameObject.SetActive(false));
        Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, tweenDelay, true);

    }

    private void ChangeCoinsText()
    {
        totalCoinsText.text = GameManager.Instance.totalCoins.ToString();
    }
}
