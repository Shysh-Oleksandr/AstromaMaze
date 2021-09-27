using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;

    private bool isMenuShown;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnPausedChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnPausedChanged;
    }

    void Start()
    {
        UIManager.Instance.pauseMenu.SetActive(false);
    }


    public void PauseGame()
    {
        if(!SceneChanger.Instance.isLoading &&
            !isMenuShown && !Tweening.Instance.isPauseMenuTweening && !SceneChanger.Instance.blackFader.enabled)
        {
            GameManager.Instance.UpdateGameState("Pause");
            UIManager.Instance.pauseMenu.gameObject.SetActive(true);
        }
        
    }

    public void ResumeGame()
    {
        GameManager.Instance.UpdateGameState("Playing");
        Tweening.Instance.TweenArrayOut(UIManager.Instance.pauseMenuElements, 0.1f, 0.1f, false);
        Tweening.Instance.TweenScale(UIManager.Instance.pauseMenu, 0.25f, false, 0.4f);
    }

    private void OnPausedChanged(GameState state)
    {
        isPaused = (state == GameState.Pause);
        isMenuShown = (state == GameState.Victory || state == GameState.Lose);
    }
}
