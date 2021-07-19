using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private const float menuY = 132f;

    [SerializeField] private Transform optionsMenu, shopMenu;
    [SerializeField]
    private GameObject[] mainMenuElements, videoElements, audioElements, gameElements, languageElements,
                                        sprayElements, bootsElements, birdsElements, compassElements;
    [SerializeField] private GameObject videoTab, audioTab, gameTab, languageTab, sprayTab, bootsTab, birdsTab, compassTab;
    [SerializeField] private float screenCenterX, tweenDuration, tweenDelay;

    private int id;
    public bool isTweening;
    private Vector2 optionsMenuStartPos, shopMenuStartPos;
    private LTDescr d;

    private void Start()
    {
        optionsMenuStartPos = new Vector2(Screen.width, menuY);
        shopMenuStartPos = new Vector2(-Screen.width, menuY);

        Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, 0.35f, true);

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
}
