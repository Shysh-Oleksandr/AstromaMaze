using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool takingAway = false;

    [SerializeField] private float timeLeft;
    public float TimeLeft { get => timeLeft; set => timeLeft = value; }

    private void Start()
    {
        TimeLeft = (int)TimeLeft * GameManager.Instance.difficultyCoefficient;
    }

    void Update()
    {
        if (!takingAway && TimeLeft > 0 && GameManager.Instance.State == GameState.Playing)
        {
            StartCoroutine(RunTimerCoroutine());
        }
    }

    IEnumerator RunTimerCoroutine()
    {
        takingAway = true;
        TimeSpan timeSpan = TimeSpan.FromSeconds(TimeLeft);
        UIManager.Instance.timerText.text = "Time left: " + timeSpan.ToString("m':'ss");

        TimeLeft--;

        yield return new WaitForSeconds(1);
        if (TimeLeft <= 0)
        {
            GameManager.Instance.UpdateGameState("Lose");
        }
        takingAway = false;
    }
}
