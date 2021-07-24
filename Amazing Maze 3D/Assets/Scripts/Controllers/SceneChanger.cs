using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : GenericSingletonClass<SceneChanger>
{
    public Animator animator;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    public bool isLoading;

    private int levelToLoad;
    private AudioSource fadingSource;

    private void Start()
    {
        foreach (Sound s in AudioManager.Instance.sounds)
        {
            if (s.name == "Fading")
            {
                fadingSource = s.source;
            }
        }
    }
    public void FadeToNextLevel()
    {
        Instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.Instance.UpdateGameState("Playing");
    }

    public void RestartLevel()
    {
        Instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.UpdateGameState("Playing");
    }

    public void OpenMainMenu()
    {
        GameManager.Instance.UpdateGameState("MainMenu");
    }


    public void FadeToLevel(int levelIndex)
    {
        AudioManager.Instance.Play("Fading");
        levelToLoad = levelIndex;
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            animator.SetTrigger("FadeOut");
        }
        Instance.isLoading = true;
    }

    public void OnFadeComplete()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        if (levelToLoad <= SceneManager.sceneCountInBuildSettings)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);

            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);

                slider.value = progress;
                progressText.text = progress.ToString("0%");

                yield return null;
            }
            fadingSource.Stop();
            Instance.isLoading = false;
            loadingScreen.SetActive(false);
        }
        else
        {
            GameManager.Instance.UpdateGameState("MainMenu");
        }


    }
}
