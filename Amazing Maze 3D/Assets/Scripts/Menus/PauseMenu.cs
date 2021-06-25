using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public Camera mainCam;

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
        GameManager.Instance.UpdateGameState("Pause");
        UIManager.Instance.pauseMenu.gameObject.SetActive(true);
        UIManager.Instance.paintingPointer.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        GameManager.Instance.UpdateGameState("Playing");
        UIManager.Instance.pauseMenu.gameObject.SetActive(false);
        UIManager.Instance.paintingPointer.gameObject.SetActive(mainCam.gameObject.activeSelf);
    }

    private void OnPausedChanged(GameState state)
    {
        isPaused = (state == GameState.Pause);
        isMenuShown = (state == GameState.Victory || state == GameState.Lose);
    }
}
