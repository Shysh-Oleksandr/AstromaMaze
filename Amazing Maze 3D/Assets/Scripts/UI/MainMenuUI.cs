using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private const float menuY = 132f;
    /*private const float ButtonStartX = 207f;
    private const float ButtonDeltaY = 781f;*/

    [SerializeField] private Transform optionsMenu, shopMenu;
    [SerializeField] private GameObject[] mainMenuElements, videoElements, audioElements, gameElements, languageElements;
    [SerializeField] private float screenCenterX, tweenDuration, tweenDelay;
    [SerializeField] private Image videoArrow, audioArrow, gameArrow, languageArrow;

    private Vector2 optionsMenuStartPos, shopMenuStartPos;

    private void Start()
    {
        optionsMenuStartPos = new Vector2(Screen.width, menuY);
        shopMenuStartPos = new Vector2(-Screen.width, menuY);

        Tweening.Instance.TweenArray(mainMenuElements, tweenDuration, tweenDelay, true);
    }

    public void TweenVideo()
    {
        if (!videoArrow.IsActive())
        {
            Tweening.Instance.TweenArray(videoElements, tweenDuration, tweenDelay, false);
        }
    }

    public void TweenAudio()
    {
        if (!audioArrow.IsActive())
        {
            Tweening.Instance.TweenArray(audioElements, tweenDuration, tweenDelay, false);

        }
    }

    public void TweenGameOption()
    {
        if (!gameArrow.IsActive())
        {
            Tweening.Instance.TweenArray(gameElements, tweenDuration, tweenDelay, false);

        }

    }

    public void TweenLanguage()
    {
        if (!languageArrow.IsActive())
        {
            Tweening.Instance.TweenArray(languageElements, tweenDuration, tweenDelay, false);

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
