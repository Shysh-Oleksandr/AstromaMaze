using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI sprayText, timerText, coinsText, levelText, surpiseText;
    public Image paintingPointer;
    public GameObject victoryMenu, replayMenu;
    public GameObject pauseMenu;
    public GameObject[] pauseMenuElements, victoryMenuElements, replayMenuElements;
    public CanvasGroup victoryMenuBg, loseMenuBg;
    public PlayerController playerController;
    public Paint paint;

    public int levelCoins = 0, totalCoins;

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
        GameManager.Instance.currentLevel = SceneManager.GetActiveScene().buildIndex - 1;

        playerController.OnCoinTakeEvent += OnCoinChanged;
        paint.OnSprayChangedEvent += OnSprayChanged;

        levelCoins = 0;
        totalCoins = GameManager.Instance.totalCoins;


        coinsText.text = "0";
        sprayText.text = ObjectPooler.instance.sprayAmount.ToString();
        levelText.text = "Level " + GameManager.Instance.currentLevel.ToString();

        Tweening.Instance.TweenVertically(levelText.gameObject, 0.6f, 0, true, true);
    }

    private void OnDestroy()
    {
        playerController.OnCoinTakeEvent -= OnCoinChanged;
        paint.OnSprayChangedEvent -= OnSprayChanged;
    }

    private void OnSprayChanged()
    {
        sprayText.text = ObjectPooler.instance.sprayAmount.ToString();
    }

    public void OnCoinChanged(int coinValue)
    {
        GameManager.Instance.totalCoins += coinValue;
        totalCoins = GameManager.Instance.totalCoins;
        levelCoins += coinValue;
        coinsText.text = levelCoins.ToString();
    }

    public void TweenPauseMenu()
    {
        Tweening.Instance.TweenScale(pauseMenu, 0.15f, true, 0f);
    }

    public void TweenTimerText()
    {
        AudioManager.Instance.Play("Heartbeat");
        timerText.fontStyle = FontStyles.Bold;
        timerText.color = new Color32(178, 24, 24, 255);
        timerText.gameObject.LeanScale(new Vector2(1.4f, 1.4f), 0.5f)
            .setEaseInOutBack()
            .setOnComplete(() =>
            {
                timerText.gameObject.LeanScale(new Vector2(1f, 1f), 0.5f)
                    .setEaseInOutBack()
                    .setOnComplete(() =>
                    {
                        timerText.fontStyle = FontStyles.Normal;
                        timerText.color = new Color32(231, 225, 225, 255);
                    });
            });

    }
}
