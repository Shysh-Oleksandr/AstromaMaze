using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool takingAway = false;
    private int[] criticalTimeArray = new int[] { 30, 20, 15, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0};

    [SerializeField] private float timeLeft;
    public float TimeLeft { get => timeLeft; set => timeLeft = value; }

    private void Start()
    {
        TimeLeft = (int)TimeLeft * GameManager.Instance.difficultyCoefficient;
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
            UIManager.Instance.TweenTimerText();
        }

        TimeLeft--;

        if (TimeLeft < 0)
        {
            GameManager.Instance.UpdateGameState("Lose");
        }
        yield return new WaitForSeconds(1);
        
        takingAway = false;
    }
}
