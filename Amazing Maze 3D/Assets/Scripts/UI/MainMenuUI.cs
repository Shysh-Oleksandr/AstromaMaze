using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private const float menuY = 132f;

    [SerializeField] private Transform optionsMenu, shopMenu;
    [SerializeField] private GameObject[] mainMenuElements, videoElements, audioElements, gameElements, languageElements;
    [SerializeField] private float screenCenterX, tweenDuration, tweenDelay;
    [SerializeField] private Image videoArrow, audioArrow, gameArrow, languageArrow;

    private Vector2 optionsMenuStartPos, shopMenuStartPos;

    private void Start()
    {
        optionsMenuStartPos = new Vector2(Screen.width, menuY);
        shopMenuStartPos = new Vector2(-Screen.width, menuY);

        Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, 0.35f, true);
    }

    private void TweenDiagonally(GameObject[] optionElements)
    {
        for (int i = 0; i < optionElements.Length; i++)
        {
            GameObject element = optionElements[i];
            Vector2 elementStartPos = element.transform.position;
            element.transform.localPosition = new Vector2(elementStartPos.x, -Screen.height);

            LeanTween.move(element, elementStartPos, tweenDuration).setEaseOutExpo().delay = tweenDelay * (i + 1);
        }
    }

    public void TweenVideo()
    {
        if (!videoArrow.IsActive())
        {
            TweenDiagonally(videoElements);
        }
    }

    public void TweenAudio()
    {
        if (!audioArrow.IsActive())
        {
            TweenDiagonally(audioElements);
        }
    }

    public void TweenGameOption()
    {
        if (!gameArrow.IsActive())
        {
            TweenDiagonally(gameElements);
        }
    }

    public void TweenLanguage()
    {
        if (!languageArrow.IsActive())
        {
            TweenDiagonally(languageElements);
        }
    }

    public void EnableOptionsMenu()
    {
        optionsMenu.gameObject.SetActive(true);

        optionsMenu.localPosition = optionsMenuStartPos;
        optionsMenu.LeanMoveLocalX(screenCenterX, tweenDuration).setEaseOutExpo();

        if (videoArrow.IsActive())
        {
            TweenVideo();
        }
        else if (audioArrow.IsActive())
        {
            TweenAudio();
        }
        else if (gameArrow.IsActive())
        {
            TweenGameOption();
        }
        else if (languageArrow.IsActive())
        {
            TweenLanguage();
        }

    }

    public void EnableShopMenu()
    {
        shopMenu.gameObject.SetActive(true);

        shopMenu.localPosition = shopMenuStartPos;
        shopMenu.LeanMoveLocalX(screenCenterX, tweenDuration).setEaseOutExpo();
    }

    public void DisableOptionsMenu()
    {
        optionsMenu.LeanMoveLocalX(optionsMenuStartPos.x, tweenDuration).setEaseInExpo().setOnComplete(OnOptionsMenuComplete);
        Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, tweenDelay, true);

    }

    public void DisableShopMenu()
    {
        shopMenu.LeanMoveLocalX(shopMenuStartPos.x, tweenDuration).setEaseInExpo().setOnComplete(OnShopMenuComplete);
        Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, tweenDelay, true);

    }

    private void OnOptionsMenuComplete()
    {
        optionsMenu.gameObject.SetActive(false);
    }

    private void OnShopMenuComplete()
    {
        shopMenu.gameObject.SetActive(false);
    }

}
