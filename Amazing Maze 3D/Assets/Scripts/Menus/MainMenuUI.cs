using UnityEngine;
using UnityEngine.UI;

public class TweeningUI : MonoBehaviour
{
    private const float menuY = 132f;
    /*private const float ButtonStartX = 207f;
    private const float ButtonDeltaY = 781f;*/

    [SerializeField] private Transform optionsMenu, shopMenu;
    [SerializeField] private GameObject[] mainMenuElements, videoElements, audioElements, gameElements, languageElements;
    [SerializeField] private float screenCenterX, tweetDuration, tweetDelay;
    [SerializeField] private Image videoArrow, audioArrow, gameArrow, languageArrow;

    private Vector2 optionsMenuStartPos, shopMenuStartPos;

    private void Start()
    {
        optionsMenuStartPos = new Vector2(Screen.width, menuY);
        shopMenuStartPos = new Vector2(-Screen.width, menuY);

        TweenDiagonally(mainMenuElements);
    }

    /*private void TweenMainMenu()
    {
        for (int i = 0; i < mainMenuElements.Length; i++)
        {
            GameObject element = mainMenuElements[i];
            Vector2 elementStartPos = new Vector2(ButtonStartX, element.transform.position.y - ButtonDeltaY);
            element.transform.localPosition = new Vector2(elementStartPos.x, -Screen.height);

            element.LeanMoveLocalY(elementStartPos.y, tweetDuration).setEaseOutExpo().delay = tweetDelay * (i+1);
        }
    }*/

    private void TweenDiagonally(GameObject[] optionElements)
    {
        for (int i = 0; i < optionElements.Length; i++)
        {
            GameObject element = optionElements[i];
            Vector2 elementStartPos = element.transform.position;
            element.transform.localPosition = new Vector2(elementStartPos.x, -Screen.height);

            LeanTween.move(element, elementStartPos, tweetDuration).setEaseOutExpo().delay = tweetDelay * (i + 1);
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
        optionsMenu.LeanMoveLocalX(screenCenterX, tweetDuration).setEaseOutExpo();
    }

    public void EnableShopMenu()
    {
        shopMenu.gameObject.SetActive(true);

        shopMenu.localPosition = shopMenuStartPos;
        shopMenu.LeanMoveLocalX(screenCenterX, tweetDuration).setEaseOutExpo();
    }

    public void DisableOptionsMenu()
    {
        optionsMenu.LeanMoveLocalX(optionsMenuStartPos.x, tweetDuration).setEaseInExpo().setOnComplete(OnOptionsMenuComplete);
    }

    public void DisableShopMenu()
    {
        shopMenu.LeanMoveLocalX(shopMenuStartPos.x, tweetDuration).setEaseInExpo().setOnComplete(OnShopMenuComplete);
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
