using UnityEngine;

public class Tweening : MonoBehaviour
{
    [SerializeField] private float tweenDuration, tweenDelay;
    [SerializeField] private bool tweenOnStart = false;
    public Vector3[] menuElementsStartPos;
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
            Tween(tweenDuration, tweenDelay);
        }
    }

    public void Tween(float tweenDuration, float tweenDelay)
    {
        Vector2 elementStartPos = transform.position;
        transform.localPosition = new Vector2(Screen.width * 1.2f, -Screen.height / 2);

        LeanTween.move(gameObject, elementStartPos, tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).delay = tweenDelay;
    }

    public void TweenArray(GameObject[] optionElements, float tweenDuration, float tweenDelay, bool startDelay)
    {
        for (int i = 0; i < optionElements.Length; i++)
        {
            GameObject element = optionElements[i];
            element.SetActive(true);
            if (!isArrayInited)
            {
                menuElementsStartPos = new Vector3[optionElements.Length];
                isArrayInited = true;
            }
            if (menuElementsStartPos[i] == Vector3.zero)
            {
                menuElementsStartPos[i] = element.transform.position;
            }

            element.transform.localPosition = new Vector2(Screen.width, -Screen.height / 2);

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
            gameObject.transform.LeanScale(Vector2.one, tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).setOnComplete(TweenPauseMenu).delay = delay;
        }
        else
        {
            gameObject.transform.localScale = Vector2.one;
            gameObject.transform.LeanScale(Vector2.zero, tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).setOnComplete(DisablePauseMenu).delay = delay;
        }
    }

    private void DisablePauseMenu()
    {
        UIManager.Instance.pauseMenu.SetActive(false);
    }

    private void TweenPauseMenu()
    {
        TweenArray(UIManager.Instance.pauseMenuElements, 0.15f, 0.15f, true);
    }
}
