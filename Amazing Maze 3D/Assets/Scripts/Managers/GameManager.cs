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

        UpdateGameState(State);
    }


    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState) // Maybe replace with State?
        {
            case GameState.Playing:
                break;
            case GameState.Pause:
                break;
            case GameState.Victory:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            case GameState.MainMenu:
                Debug.Log("Menu");
                break;
            case GameState.LevelSelection:
                Debug.Log("LevelSelection");
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleLose()
    {
        isGameRunning = false;

        UIManager.Instance.loseText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HandleWin()
    {
        isGameRunning = false;

        UIManager.Instance.winnerText.gameObject.SetActive(true);

        Time.timeScale = 0f;
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