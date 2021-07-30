using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera mainCam;
    public Camera birdsEyeCam;
    public SpellCooldown spellCooldown;

    public float nextBirdsEyeView;

    public BirdsEyeViewItem birdsEyeViewItem;

    bool isMainCam;


    private void Start()
    {
        spellCooldown.cooldownTime = birdsEyeViewItem.birdsEyeViewCooldown;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !spellCooldown.isCooldown && GameManager.Instance.isGameRunning && !birdsEyeCam.gameObject.activeSelf)
        {
            var tempColor = spellCooldown.skillImage.color;
            tempColor.a = 0.45f;
            spellCooldown.skillImage.color = tempColor;
            SwitchCameras();
        }
    }


    public void SwitchCameras()
    {
        isMainCam = mainCam.gameObject.activeSelf;

        if (isMainCam)
        {
            mainCam.gameObject.SetActive(false);
            birdsEyeCam.gameObject.SetActive(true);

            UIManager.Instance.paintingPointer.gameObject.SetActive(false);
        }
        else
        {
            mainCam.gameObject.SetActive(true);
            birdsEyeCam.gameObject.SetActive(false);

            UIManager.Instance.paintingPointer.gameObject.SetActive(true);
        }

    }
}
