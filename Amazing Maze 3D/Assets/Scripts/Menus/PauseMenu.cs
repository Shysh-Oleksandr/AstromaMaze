using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

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
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !SceneChanger.Instance.isLoading && !isMenuShown)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        GameManager.Instance.UpdateGameState("Pause");
        UIManager.Instance.paintingPointer.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        GameManager.Instance.UpdateGameState("Playing");
        UIManager.Instance.paintingPointer.gameObject.SetActive(true);
    }

    private void OnPausedChanged(GameState state)
    {
        isPaused = (state == GameState.Pause);
        isMenuShown = (state == GameState.Victory || state == GameState.Lose);
    }
}
