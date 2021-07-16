using TMPro;
using UnityEngine;

public class Tweening : MonoBehaviour
{
    [SerializeField] private float tweenDuration, tweenDelay;
    [SerializeField] private bool tweenOnStart = false;
    private Vector3[] pauseMenuElementsStartPos, menuElementsStartPos;
    private bool isArrayInited;

    #region Singleton 
    private static Tweening instance;

    public static Tweening Instance { get { return instance; } }


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

    void Start()
    {
        if (tweenOnStart)
        {
            Tween(gameObject, tweenDuration, tweenDelay);
        }
    }

    public void Tween(GameObject optionElements, float tweenDuration, float tweenDelay)
    {
        Vector2 elementStartPos = optionElements.transform.position;
        optionElements.transform.localPosition = new Vector2(0, -Screen.height);

        LeanTween.move(gameObject, elementStartPos, tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).delay = tweenDelay;
    }

    public void TweenArray(GameObject[] optionElements, float tweenDuration, float tweenDelay, bool startDelay)
    {
        for (int i = 0; i < optionElements.Length; i++)
        {
            GameObject element = optionElements[i];
            menuElementsStartPos = new Vector3[optionElements.Length];

            if (menuElementsStartPos[i] == Vector3.zero)
            {
                menuElementsStartPos[i] = element.transform.position;
            }

            element.transform.localPosition = new Vector2(Screen.width, -Screen.height / 2);
            element.SetActive(true);

            if (startDelay)
            {
                LeanTween.move(element, menuElementsStartPos[i], tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).delay = tweenDelay * (i + 1);
            }
            else
            {
                LeanTween.move(element, menuElementsStartPos[i], tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).delay = tweenDelay * i;
            }
        }
    }

    public void TweenPauseMenuElements(GameObject[] optionElements, float tweenDuration, float tweenDelay, bool startDelay)
    {
        for (int i = 0; i < optionElements.Length; i++)
        {
            GameObject element = optionElements[i];
            if (!isArrayInited)
            {
                pauseMenuElementsStartPos = new Vector3[optionElements.Length];
                isArrayInited = true;
            }
            if (pauseMenuElementsStartPos[i] == Vector3.zero)
            {
                pauseMenuElementsStartPos[i] = element.transform.position;
            }

            element.transform.localPosition = new Vector2(Screen.width, -Screen.height / 2);
            element.SetActive(true);

            if (startDelay)
            {
                LeanTween.move(element, pauseMenuElementsStartPos[i], tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).delay = tweenDelay * (i + 1);
            }
            else
            {
                LeanTween.move(element, pauseMenuElementsStartPos[i], tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).delay = tweenDelay * i;
            }
        }
    }

    public void TweenArrayOut(GameObject[] optionElements, float tweenDuration, float tweenDelay, bool startDelay)
    {
        for (int i = optionElements.Length; i > 0; i--)
        {
            GameObject element = optionElements[i - 1];

            Vector2 elementPosTo = new Vector2(Screen.width, -Screen.height / 2);

            if (startDelay)
            {
                LeanTween.move(element, elementPosTo, tweenDuration)
                    .setEaseOutExpo()
                    .setIgnoreTimeScale(true)
                    .setOnComplete(() =>
                    {
                        element.SetActive(false);
                    })
                    .delay = tweenDelay * (optionElements.Length - i + 1);
            }
            else
            {
                LeanTween.move(element, elementPosTo, tweenDuration)
                    .setEaseOutExpo()
                    .setIgnoreTimeScale(true)
                    .setOnComplete(() =>
                    {
                        element.SetActive(false);
                    })
                    .delay = tweenDelay * (optionElements.Length - i);
            }
        }
    }

    public void TweenScale(GameObject gameObject, float tweenDuration, bool scaleIn, float delay)
    {
        if (scaleIn)
        {
            gameObject.transform.localScale = Vector2.zero;
            gameObject.transform.LeanScale(Vector2.one, tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true)
                .setOnComplete(() => TweenPauseMenuElements(UIManager.Instance.pauseMenuElements, 0.15f, 0.15f, true))
                .delay = delay;
        }
        else
        {
            gameObject.transform.localScale = Vector2.one;
            gameObject.transform.LeanScale(Vector2.zero, tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true)
                .setOnComplete(() => UIManager.Instance.pauseMenu.SetActive(false))
                .delay = delay;
        }
    }

    public void TweenVictoryLoseMenu(GameObject menu, CanvasGroup bg, GameObject[] menuElements)
    {
        bg.alpha = 0;
        bg.gameObject.SetActive(true);
        bg.LeanAlpha(1f, 1f).setIgnoreTimeScale(true);

        menu.gameObject.SetActive(true);
        menu.transform.localPosition = new Vector2(0, -Screen.height);
        menu.LeanMoveLocalY(0, 1f).setEaseOutBack()
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                TweenArray(menuElements, 0.5f, 0.2f, true);
            })
            .delay = 0.7f;
    }

    public void TweenVertically(GameObject go, float duration, float delay, bool tweenBack, bool tweenUp)
    {
        go.gameObject.SetActive(true);

        go.gameObject.transform.localPosition = new Vector2(0, -Screen.height / 2);
        go.gameObject.LeanMoveLocalY(0, duration).setEaseOutCubic()
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                if (tweenBack)
                {
                    if (tweenUp)
                    {
                        go.gameObject.LeanMoveLocalY(Screen.height, duration).setEaseInBack().setOnComplete(() => go.gameObject.SetActive(false));
                    }
                    else
                    {
                        go.gameObject.LeanMoveLocalY(-Screen.height, duration).setEaseInBack().setOnComplete(() => go.gameObject.SetActive(false));
                    }
                }
            })
            .delay = delay;

    }
}