using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SceneChanger : GenericSingletonClass<SceneChanger>
{
    public Animator animator;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    private int levelToLoad;

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");

        switch (levelIndex)
        {
            case 0:
                GameManager.Instance.UpdateGameState(GameState.MainMenu);
                break;
            
        }
    }

    public void OnFadeComplete()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress.ToString("0.0%");

            yield return null;
        }

        loadingScreen.SetActive(false);

    }
}
