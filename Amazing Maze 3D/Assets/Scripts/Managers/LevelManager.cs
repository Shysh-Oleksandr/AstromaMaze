using UnityEngine;
using TMPro;
using System;

[Serializable]
public struct SpawnPositions
{
    public Transform spawnPosition;
    public int[] suitableLevels;
}

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    public SpawnPositions[] spawnPositions;

    public TextMeshProUGUI lockLevelText;
    public Transform playerPosition;

    public int maxLevelReached;

    private Color passedColor = new Color32(52, 217, 63, 155);
    private Color reachedColor = new Color32(212, 236, 45, 155);
    private Color lockedColor = new Color32(202, 10, 10, 155);

    private void Start()
    {
        maxLevelReached = GameManager.Instance.maxLevelReached;
        ChangeLevelTextColor();
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        for (int pos = 0; pos < spawnPositions.Length; pos++)
        {
            for (int level = 0; level < spawnPositions[pos].suitableLevels.Length; level++)
            {
                if(spawnPositions[pos].suitableLevels[level] == maxLevelReached)
                {
                    playerPosition.position = spawnPositions[pos].spawnPosition.position;
                    print(pos + " " + level);
                    break;
                }
            }
        }

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
