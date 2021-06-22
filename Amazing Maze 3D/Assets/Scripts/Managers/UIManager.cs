using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI sprayText, timerText, loseText, coinsText, winnerText;

    public static event Action OnUIChanged;

    #region Singleton 
    private static UIManager instance;

    public static UIManager Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion


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
