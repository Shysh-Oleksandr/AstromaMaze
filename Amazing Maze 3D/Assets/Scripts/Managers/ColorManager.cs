using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public Button colorArrowButton;
    public GameObject colorPanel;
    public GameObject[] colors;

    void Start()
    {
        colorArrowButton.onClick.AddListener(() => ShowHideColorPanel());
    }

    public void ShowHideColorPanel()
    {
        print("Btn");
        colorPanel.SetActive(!colorPanel.activeInHierarchy);
    }
}
