using System;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public float totalCoins;
    public float difficultyCoefficient = 1f;
    public bool isGameRunning = true;

    private void Start()
    {
        isGameRunning = true;
    }

    public GameState SetEnum(string newState)
    {
        State = (GameState)Enum.Parse(typeof(GameState), newState);
        return State;
    }

    public void UpdateGameState(string newState)
    {
        State = SetEnum(newState);
        Debug.Log(State);

        switch (State)
        {
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.Pause:
                HandlePause();
                break;
            case GameState.Victory:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.LevelSelection:
                HandleLevelSelection();
                break;
        }

        OnGameStateChanged?.Invoke(State);
    }

    private void HandleLevelSelection()
    {
        SceneChanger.Instance.FadeToLevel(1);
    }

    private void HandleMainMenu()
    {
        Time.timeScale = 1f;
        SceneChanger.Instance.FadeToLevel(0);
    }

    private void HandlePause()
    {
        Time.timeScale = 0f;
        isGameRunning = false;
        UIManager.Instance.TweenPauseMenu();
    }

    private void HandlePlaying()
    {
        Time.timeScale = 1f;
        isGameRunning = true;
    }

    private void HandleLose()
    {
        isGameRunning = false;
        Time.timeScale = 0f;
        Tweening.Instance.TweenVictoryLoseMenu(UIManager.Instance.replayMenu, UIManager.Instance.loseMenuBg, UIManager.Instance.replayMenuElements);
    }


    private void HandleWin()
    {
        isGameRunning = false;
        Time.timeScale = 0f;
        Tweening.Instance.TweenVictoryLoseMenu(UIManager.Instance.victoryMenu, UIManager.Instance.victoryMenuBg, UIManager.Instance.victoryMenuElements);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
