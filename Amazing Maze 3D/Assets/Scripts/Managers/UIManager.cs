using TMPro;

public class UIManager : GenericSingletonClass<UIManager>
{
    public TextMeshProUGUI sprayText, timerText, loseText, coinsText, winnerText;

    void Start()
    {
        coinsText.text = "Coins: " + GameManager.Instance.totalCoins;
        sprayText.text = "Spray: " + ObjectPooler.instance.sprayAmount;
    }

    void Update()
    {
        sprayText.text = "Spray: " + ObjectPooler.instance.sprayAmount;

    }
}
