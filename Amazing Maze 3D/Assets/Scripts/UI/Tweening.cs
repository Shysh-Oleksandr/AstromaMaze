using UnityEngine;

public class Tweening : MonoBehaviour
{
    [SerializeField] private float tweenDuration, tweenDelay;
    [SerializeField] private bool tweenOnStart = false;
    public float startX;

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
        transform.localPosition = new Vector2(elementStartPos.x, -Screen.height / 2);

        LeanTween.move(gameObject, elementStartPos, tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).delay = tweenDelay;
    }

    public void TweenArray(GameObject[] optionElements, float tweenDuration, float tweenDelay, bool startDelay)
    {
        for (int i = 0; i < optionElements.Length; i++)
        {
            GameObject element = optionElements[i];
            element.SetActive(true);
            Vector2 elementStartPos = element.transform.position;
            if (startX == 0)
            {
                startX = elementStartPos.x;
            }

            element.transform.localPosition = new Vector2(Screen.width, -Screen.height);

            if (startDelay)
            {
                LeanTween.move(element, elementStartPos, tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).delay = tweenDelay * (i + 1);
            }
            else
            {
                LeanTween.move(element, elementStartPos, tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).delay = tweenDelay * i;
            }
        }
    }

    public void TweenArrayOut(GameObject[] optionElements, float tweenDuration, float tweenDelay, bool startDelay)
    {
        for (int i = optionElements.Length; i > 0; i--)
        {
            GameObject element = optionElements[i - 1];

            Vector2 elementPosTo = new Vector2(startX, -Screen.height / 2);

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

    public void TweenScale(GameObject gameObject, float tweenDuration, bool scaleIn)
    {
        if (scaleIn)
        {
            gameObject.transform.localScale = Vector2.zero;
            gameObject.transform.LeanScale(Vector2.one, tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).setOnComplete(TweenPauseMenu);
        }
        else
        {
            gameObject.transform.localScale = Vector2.one;
            gameObject.transform.LeanScale(Vector2.zero, tweenDuration).setEaseOutExpo().setIgnoreTimeScale(true).setOnComplete(DisablePauseMenu);
        }
    }

    private void DisablePauseMenu()
    {
        UIManager.Instance.pauseMenu.SetActive(false);
    }

    private void TweenPauseMenu()
    {
        TweenArray(UIManager.Instance.pauseMenuElements, 0.3f, 0.2f, true);
    }
}
