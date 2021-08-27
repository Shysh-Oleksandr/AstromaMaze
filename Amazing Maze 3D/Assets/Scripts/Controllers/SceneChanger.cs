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
    public Image starLoadingBg, loadingSliderImage, blackFader;

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

    public void ChangeStarsBgColor()
    {
        foreach (Image image in MainMenuUI.Instance.starsBgImages)
        {
            if (image != null)
            {
                image.color = SetRandomHue(image, true);
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
        SetRandomHue(starLoadingBg, true); // Changing loading bg hue and size.
        SetRandomHue(loadingSliderImage, false); // Changing loading bar hue.
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

    public Color SetRandomHue(Image image, bool changeScale)
    {
        float h, s, v;
        float H = Random.Range(0, 1.0f);
        float a = image.color.a;
        

        Color.RGBToHSV(image.color, out h, out s, out v);
        image.color = Color.HSVToRGB(H, s, v);
        Color startColor = image.color;

        startColor.a = a;
        image.color = startColor;

        if (changeScale)
        {
            float xScale = Random.Range(1.0f, 2.0f);
            float yScale = Random.Range(1.0f, 2.0f);
            int flip = Random.Range(0, 2);
            Vector3 scale = image.gameObject.transform.localScale;

            scale.x = xScale;
            scale.y = yScale;
            image.gameObject.transform.localScale = scale;

            if (flip == 1)
            {
                image.gameObject.transform.eulerAngles = new Vector3(
                    image.gameObject.transform.eulerAngles.x,
                    image.gameObject.transform.eulerAngles.y,
                    180
                );
            }
        }
        
        return image.color;
    }
}
