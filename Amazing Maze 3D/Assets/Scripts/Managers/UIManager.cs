using System;
using TMPro;
using UnityEngine;
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

    private int levelCoins = 0;

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
        playerController.OnCoinTakeEvent += OnCoinChanged;
        paint.OnSprayChangedEvent += OnSprayChanged;

        levelCoins = 0;

        coinsText.text = "0";
        sprayText.text = ObjectPooler.instance.sprayAmount.ToString();
        levelText.text = "Level 1"; // TODO: + level variable.

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
