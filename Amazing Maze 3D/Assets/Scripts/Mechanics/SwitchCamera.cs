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
        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextBirdsEyeView && GameManager.Instance.isGameRunning)
        {
            nextBirdsEyeView = Time.time + birdsEyeViewCooldown;

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
