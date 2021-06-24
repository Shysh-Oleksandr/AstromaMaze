using System;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public float totalCoins;
    public bool isGameRunning = true;

    private void Start()
    {
        isGameRunning = true;

        /*        UpdateGameState(State.ToString());
        */
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
    }

    private void HandlePlaying()
    {
        Time.timeScale = 1f;
        isGameRunning = true;
    }

    private void HandleLose()
    {
        isGameRunning = false;

        UIManager.Instance.loseText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void HandleWin()
    {
        isGameRunning = false;

        UIManager.Instance.victoryMenu.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

public enum GameState
{
    MainMenu,
    LevelSelection,
    Playing,
    Pause,
    Victory,
    Lose
}