using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsEyeView : MonoBehaviour
{
    public Camera mainCam;
    public Camera birdsEyeCam;

    bool isMainCam;

    // It is called in the animation clip.
    public void ShowBirdsEyeView()
    {
        isMainCam = mainCam.gameObject.activeSelf;

        if (isMainCam)
        {
            mainCam.gameObject.SetActive(false);
            birdsEyeCam.gameObject.SetActive(true);
        }
        else
        {
            mainCam.gameObject.SetActive(true);
            birdsEyeCam.gameObject.SetActive(false);
        }

    }
}
