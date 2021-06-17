using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera mainCam;
    public Camera birdsEyeCam;

    [SerializeField] float birdsEyeViewCooldown;
    float nextBirdsEyeView;
    bool isMainCam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextBirdsEyeView)
        {
            nextBirdsEyeView = Time.time + birdsEyeViewCooldown;

            ShowBirdsEyeView();
        }
    }


    private void ShowBirdsEyeView()
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
