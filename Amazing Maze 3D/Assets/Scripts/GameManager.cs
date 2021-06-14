using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sprayText, timerText;
    private float startTime;
    [SerializeField] float timeLeft = 88;
    private float secondsLeft, minutesLeft;
    bool takingAway = false;

    private void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if(!takingAway && timeLeft >= 0)
        {
            StartCoroutine("TimerTake");
        }
        sprayText.text = "Spray: " + ObjectPooler.instance.sprayAmount;
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

        yield return new WaitForSeconds(1);
        timeLeft--;

        takingAway = false;
    }
}
