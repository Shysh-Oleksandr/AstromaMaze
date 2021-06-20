using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sprayText, timerText, loseText, coinsText;
    [SerializeField] float timeLeft;
    private float secondsLeft, minutesLeft, totalCoins;
    bool takingAway = false;
    public static bool isGameRunning = true;

    private void Start()
    {
        isGameRunning = true;
        coinsText.text = "Coins: " + totalCoins;
    }

    void Update()
    {
        if(!takingAway && timeLeft >= 0)
        {
            StartCoroutine("TimerTake");
        }

        if(timeLeft < 0)
        {
            Lose();
        }


        sprayText.text = "Spray: " + ObjectPooler.instance.sprayAmount;
    }

    private void Lose()
    {
        isGameRunning = false;

        Debug.Log("Lose!");
        loseText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        minutesLeft = (int)timeLeft / 60;
        secondsLeft = timeLeft % 60;

        if (secondsLeft < 10)
        {
            timerText.text = "Time left: " + minutesLeft + ":0" + secondsLeft;
        }
        else
        {
            timerText.text = "Time left: " + minutesLeft + ":" + secondsLeft;
        }

        timeLeft--;
        yield return new WaitForSeconds(1);

        takingAway = false;
    }
}
