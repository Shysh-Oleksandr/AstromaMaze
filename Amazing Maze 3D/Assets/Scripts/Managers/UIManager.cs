using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI sprayText, timerText, loseText, coinsText, winnerText;
    public Image paintingPointer;
    public GameObject victoryMenu, replayMenu;
    public GameObject pauseMenu;
    public GameObject[] pauseMenuElements, victoryMenuElements, replayMenuElements;
    
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
        coinsText.text = GameManager.Instance.totalCoins.ToString();
        sprayText.text = ObjectPooler.instance.sprayAmount.ToString();

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
        sprayText.text = ObjectPooler.instance.sprayAmount.ToString();
    }


    public void PickupCoin(int coinValue)
    {
        GameManager.Instance.totalCoins += coinValue;
        coinsText.text = GameManager.Instance.totalCoins.ToString();
    }

    public void TweenPauseMenu()
    {
        Tweening.Instance.TweenScale(pauseMenu, 0.4f, true);
    }
}
