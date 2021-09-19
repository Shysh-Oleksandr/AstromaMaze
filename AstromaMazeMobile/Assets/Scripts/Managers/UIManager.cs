using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[Serializable]
public struct CheckpointArrows
{
    public GameObject checkpoint;
    public MeshRenderer[] arrows;
}


public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerText, coinsText, levelText, surpiseText;
    public Image[] heartIcons;
    public GameObject victoryMenu, replayMenu;
    public GameObject pauseMenu;
    public GameObject[] pauseMenuElements, victoryMenuElements, replayMenuElements;
    public CanvasGroup victoryMenuBg, loseMenuBg;
    public CheckpointArrows[] checkpointArrows;

    public PlayerController playerController;
    public Checkpoint checkpoint;
    public SlowTimeItem slowTimeItem;
    public SlowTime slowTime;

    public int levelCoins = 0, totalCoins;

    private Image starsPauseBg;
    public Image iceFrame;
    [HideInInspector]
    public Color passedCheckpointColor = new Color32(25, 255, 0, 103);

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
        levelCoins = 0;
        totalCoins = GameManager.Instance.totalCoins;

        playerController.OnCoinTakeEvent += OnCoinChanged;

        // For boss level---------------------------
        if (heartIcons.Length > 0)
        {
            GameManager.Instance.isBossLevel = true;
            GameManager.Instance.lives = 3;
        }
        else
        {
            GameManager.Instance.isBossLevel = false;
        }

        if (GameManager.Instance.isBossLevel)
        {
            OnHeartsChanged(GameManager.Instance.lives);
            levelText.text = "Boss level " + checkpoint.bossLevelIndex;
        }
        else
        {
            levelText.text = "Level " + GameManager.Instance.currentLevel.ToString();
        }
        // ----------------------
        coinsText.text = "0";

        Tweening.Instance.TweenVertically(levelText.gameObject, 0.6f, 0, true, true);

        starsPauseBg = pauseMenu.GetComponentsInChildren<Image>()[1];
    }

    private void OnDestroy()
    {
        playerController.OnCoinTakeEvent -= OnCoinChanged;
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

    public void OnCoinChanged(int coinValue)
    {
        GameManager.Instance.totalCoins += coinValue;
        totalCoins = GameManager.Instance.totalCoins;
        levelCoins += coinValue;
        coinsText.text = levelCoins.ToString();
    }

    public void TweenPauseMenu()
    {
        SceneChanger.Instance.SetRandomHue(starsPauseBg, true);
        Tweening.Instance.TweenScale(pauseMenu, 0.15f, true, 0f);
    }

    public void TweenTimerText(Color32 color)
    {
        AudioManager.Instance.Play("Heartbeat");
        timerText.fontStyle = FontStyles.Bold;
        timerText.color = color;
        timerText.gameObject.LeanScale(new Vector2(3.22f, 3.22f), slowTime.isSlowTimeMode ? 0.5f * slowTimeItem.slowCoefficient : 0.5f )
            .setEaseInOutBack()
            .setOnComplete(() =>
            {
                timerText.gameObject.LeanScale(new Vector2(2.3f, 2.3f), slowTime.isSlowTimeMode ? 0.5f * slowTimeItem.slowCoefficient : 0.5f)
                    .setEaseInOutBack()
                    .setOnComplete(() =>
                    {
                        timerText.fontStyle = FontStyles.Normal;
                        timerText.color = new Color32(231, 225, 225, 255);
                    });
            });

    }
}
