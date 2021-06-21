using TMPro;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{

    public TextMeshProUGUI sprayText, timerText, loseText, coinsText, winnerText;
    public Timer timer;

    [SerializeField] private float totalCoins;
    public bool isGameRunning = true;

    private void Start()
    {
        isGameRunning = true;
        coinsText.text = "Coins: " + totalCoins;
    }

    void Update()
    {
        if (timer.TimeLeft < 0)
        {
            Lose();
        }

        sprayText.text = "Spray: " + ObjectPooler.instance.sprayAmount;
    }

    private void Lose()
    {
        isGameRunning = false;

        loseText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        isGameRunning = false;

        Debug.Log("Win!");
        winnerText.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }


}
