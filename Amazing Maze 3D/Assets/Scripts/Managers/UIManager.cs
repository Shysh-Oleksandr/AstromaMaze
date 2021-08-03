using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI sprayText, timerText, coinsText, levelText, surpiseText;
    public Image paintingPointer;
    public Image[] heartIcons;
    public GameObject victoryMenu, replayMenu;
    public GameObject pauseMenu;
    public GameObject[] pauseMenuElements, victoryMenuElements, replayMenuElements;
    public CanvasGroup victoryMenuBg, loseMenuBg;

    public PlayerController playerController;
    public Checkpoint checkpoint;
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

        if (heartIcons.Length > 0)
        {
            GameManager.Instance.isBossLevel = true;
            GameManager.Instance.lives = 3;
        }
        else
        {
            GameManager.Instance.isBossLevel = false;
        }

        levelCoins = 0;
        totalCoins = GameManager.Instance.totalCoins;

        if (GameManager.Instance.isBossLevel)
        {
            OnHeartsChanged(GameManager.Instance.lives);
            levelText.text = "Boss level " + checkpoint.bossLevelIndex;
        }
        else
        {
            levelText.text = "Level " + GameManager.Instance.currentLevel.ToString();
        }
        coinsText.text = "0";
        sprayText.text = ObjectPooler.instance.sprayAmount.ToString();

        Tweening.Instance.TweenVertically(levelText.gameObject, 0.6f, 0, true, true);
    }

    private void OnDestroy()
    {
        playerController.OnCoinTakeEvent -= OnCoinChanged;
        paint.OnSprayChangedEvent -= OnSprayChanged;
    }

    public void OnHeartsChanged(int lives)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if(i < lives)
            {
                heartIcons[i].enabled = true;
            }
            else
            {
                heartIcons[i].enabled = false;
            }
        }
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

    public void TweenTimerText(Color32 color)
    {
        AudioManager.Instance.Play("Heartbeat");
        timerText.fontStyle = FontStyles.Bold;
        timerText.color = color;
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
