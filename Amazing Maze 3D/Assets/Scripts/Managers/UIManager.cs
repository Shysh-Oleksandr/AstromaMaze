using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI sprayText, timerText, loseText, coinsText, winnerText;
    public Image paintingPointer;
    public GameObject victoryMenu;

    //public static event Action OnUIChanged;

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

        //OnUIChanged += OnSprayChanged;
    }

    private void OnDestroy()
    {
        //OnUIChanged -= OnSprayChanged;
    }

    private void Update()
    {
        OnSprayChanged();
    }

    private void OnSprayChanged()
    {
        sprayText.text = "Spray: " + ObjectPooler.instance.sprayAmount;
    }


    public void PickupCoin(int coinValue)
    {
        GameManager.Instance.totalCoins += coinValue;
        coinsText.text = "Coins: " + GameManager.Instance.totalCoins;
    }
}
