using System;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public int totalCoins;
    public float difficultyCoefficient = 1f;    
    public int maxLevelReached;
    public bool isGameRunning = true;

    private void Start()
    {
        isGameRunning = true;
        LoadGameData();
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
        SaveSystem.SaveGame(this);
        LoadGameData();

        SceneChanger.Instance.FadeToLevel(1);
    }

    private void HandleMainMenu()
    {
        SaveSystem.SaveGame(this);
        LoadGameData();

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
        AudioManager.Instance.Play("Lose");
        SaveSystem.SaveGame(this);
        isGameRunning = false;
        Time.timeScale = 0f;
        Tweening.Instance.TweenVictoryLoseMenu(UIManager.Instance.replayMenu, UIManager.Instance.loseMenuBg, UIManager.Instance.replayMenuElements);
    }


    private void HandleWin()
    {
        AudioManager.Instance.Play("Win");
        SaveSystem.SaveGame(this);
        isGameRunning = false;
        Time.timeScale = 0f;
        Tweening.Instance.TweenVictoryLoseMenu(UIManager.Instance.victoryMenu, UIManager.Instance.victoryMenuBg, UIManager.Instance.victoryMenuElements);
    }

    public void QuitGame()
    {
        AudioManager.Instance.Play("Click");
        Application.Quit();
    }

    private void LoadGameData()
    {
        GameData data = SaveSystem.LoadGame();

        totalCoins = data.totalCoins;
        difficultyCoefficient = data.difficultyCoefficient;
        maxLevelReached = data.maxLevelReached;
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveGame(this);
    }
}
