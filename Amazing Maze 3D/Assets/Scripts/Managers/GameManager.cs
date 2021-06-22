using System;
using UnityEngine;
using UnityEngine.UI;

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
*/   }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            UpdateGameState("MainMenu");
        }
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
                SceneChanger.Instance.FadeToLevel(0);
                break;
            case GameState.LevelSelection:
                SceneChanger.Instance.FadeToLevel(1);
                break;
        }

        OnGameStateChanged?.Invoke(State);
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