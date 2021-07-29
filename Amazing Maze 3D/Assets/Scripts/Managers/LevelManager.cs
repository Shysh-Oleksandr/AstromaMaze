using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;

    public TextMeshProUGUI lockLevelText;

    private Color passedColor = new Color32(52, 217, 63, 155);
    private Color reachedColor = new Color32(212, 236, 45, 155);
    private Color lockedColor = new Color32(202, 10, 10, 155);

    private void Start()
    {
        ChangeLevelTextColor();
    }

    private void ChangeLevelTextColor()
    {
        foreach (GameObject level in levels)
        {
            LevelIndex levelIndexComponent = level.GetComponentInChildren<LevelIndex>();
            int levelIndex = levelIndexComponent.levelIndex - 1;
            if (levelIndex < GameManager.Instance.maxLevelReached)
            {
                levelIndexComponent.isLocked = false;
                foreach (Transform child in level.transform)
                {
                    if (child.GetComponent<BoxCollider>() == null)
                    {
                        Renderer rend = child.GetComponent<Renderer>();
                        Material mat = Instantiate(rend.material);
                        mat.SetColor("_BaseColor", passedColor);
                        rend.material = mat;
                    }
                }
            }
            else if (levelIndex == GameManager.Instance.maxLevelReached)
            {
                levelIndexComponent.isLocked = false;
                foreach (Transform child in level.transform)
                {
                    if (child.GetComponent<BoxCollider>() == null)
                    {
                        Renderer rend = child.GetComponent<Renderer>();
                        Material mat = Instantiate(rend.material);
                        mat.SetColor("_BaseColor", reachedColor);
                        rend.material = mat;
                    }
                }
            }
            else
            {
                levelIndexComponent.isLocked = true;
                foreach (Transform child in level.transform)
                {
                    if (child.GetComponent<BoxCollider>() == null)
                    {
                        Renderer rend = child.GetComponent<Renderer>();
                        Material mat = Instantiate(rend.material);
                        mat.SetColor("_BaseColor", lockedColor);
                        rend.material = mat;
                    }
                }
            }
        }
    }
}
