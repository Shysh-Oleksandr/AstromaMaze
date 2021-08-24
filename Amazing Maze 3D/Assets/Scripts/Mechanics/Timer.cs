using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool isNextAddingColor, isNextCriticalColor;
    [SerializeField] private int[] submazeTimers; // 0 means a second subMaze, 1 - a third one... 
    [SerializeField] private Compass compass;
    private bool takingAway = false;
    private int[] criticalTimeArray = new int[] { 30, 20, 15, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0};

    private float startTime;
    public GameObject player;
    private PlayerController playerController;
    private Color32 criticalTimeColor, addingTimeColor;

    [SerializeField] private float timeLeft;
    public float TimeLeft { get => timeLeft; set => timeLeft = value; }

    private void Start()
    {
        if (GameManager.Instance.isBossLevel)
        {
            playerController = player.GetComponent<PlayerController>();
        }

        criticalTimeColor = new Color32(178, 24, 24, 255);
        addingTimeColor = new Color32(46, 178, 24, 255);
        TimeLeft = (int)TimeLeft * GameManager.Instance.difficultyCoefficient;
        
        for (int i = 0; i < submazeTimers.Length; i++)
        {
            submazeTimers[i] = Mathf.RoundToInt(submazeTimers[i] * GameManager.Instance.difficultyCoefficient);
        }
        startTime = TimeLeft;
    }

    void Update()
    {
        if (!takingAway && TimeLeft >= 0 && GameManager.Instance.State == GameState.Playing && !SceneChanger.Instance.isLoading)
        {
            StartCoroutine(RunTimerCoroutine());
        }
    }

    IEnumerator RunTimerCoroutine()
    {
        takingAway = true;
        TimeSpan timeSpan = TimeSpan.FromSeconds(TimeLeft);
        UIManager.Instance.timerText.text = timeSpan.ToString("m':'ss");

        if(criticalTimeArray.Contains((int)TimeLeft))
        {
            UIManager.Instance.TweenTimerText(criticalTimeColor);
        }

        TimeLeft--;

        if (TimeLeft < 0)
        {
            if(GameManager.Instance.isBossLevel && GameManager.Instance.lives > 1)
            {
                AudioManager.Instance.Play("Death"); // Play sound.
                GameManager.Instance.lives--; // Subtract a life.
                UIManager.Instance.OnHeartsChanged(GameManager.Instance.lives); // Changed lives UI.
                playerController.isReplacing = true;
                player.transform.position = playerController.lastCheckpointPosition; // Respawn player on last checkpoint position.
                playerController.isReplacing = false;
                StartCoroutine(compass.RotateToNorth());

                // Give the subMazed time to player.
                if (playerController.lastCheckpoint == null) 
                {
                    TimeLeft += startTime;
                }
                else
                {
                    TimeLeft += submazeTimers[playerController.lastCheckpoint.checkpointIndex];
                }
                isNextCriticalColor = true;
            }
            else if(GameManager.Instance.isBossLevel && GameManager.Instance.lives == 1)
            {
                GameManager.Instance.lives--; // Subtract a life.
                UIManager.Instance.OnHeartsChanged(GameManager.Instance.lives); // Changed lives UI.
                GameManager.Instance.UpdateGameState("Lose");
            }
            else
            {
                GameManager.Instance.UpdateGameState("Lose");
            }
        }
        if (isNextAddingColor)
        {
            UIManager.Instance.TweenTimerText(addingTimeColor);
            isNextAddingColor = false;
        }
        else if (isNextCriticalColor)
        {
            UIManager.Instance.TweenTimerText(criticalTimeColor);
            isNextCriticalColor = false;
        }
        yield return new WaitForSeconds(1);
        takingAway = false;
    }

    public void UpdateSubmazeTimer(int submazeTimerIndex)
    {
        TimeLeft += submazeTimers[submazeTimerIndex];
        isNextAddingColor = true;
    }
}
