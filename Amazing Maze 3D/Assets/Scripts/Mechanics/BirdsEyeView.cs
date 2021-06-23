using System.Collections;
using UnityEngine;

public class BirdsEyeView : MonoBehaviour
{
    public AnimationCurve upEasing, downEasing;

    public Camera mainCam;
    public Camera birdsEyeCam;
    public Transform playerTransform;
    public SwitchCamera switchCamera;

    [SerializeField] [Range(1, 100)] private float maxHeight;
    [SerializeField] [Range(0, 15)] private float coroutineDuration;
    [SerializeField] [Range(0, 5)] private float maxHeightDuration;
    [SerializeField] [Range(0, 5)] private float startOffset;

    bool isMainCam;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void OnEnable()
    {
        startPosition = transform.position;

        StartCoroutine(BirdsEyeViewCoroutine());
    }


    IEnumerator BirdsEyeViewCoroutine()
    {
        for (float i = 0; i < 1; i += Time.deltaTime / coroutineDuration)
        {
            targetPosition = playerTransform.position + Vector3.up * maxHeight;
            transform.position = Vector3.Lerp(startPosition, targetPosition, upEasing.Evaluate(i));
            yield return null;
        }

        yield return new WaitForSeconds(maxHeightDuration);

        startPosition = transform.position;
        targetPosition = playerTransform.position;

        for (float i = 0; i < 1; i += Time.deltaTime / coroutineDuration)
        {
            targetPosition = playerTransform.position + Vector3.up * startOffset;
            transform.position = Vector3.Lerp(startPosition, targetPosition, downEasing.Evaluate(i));
            yield return null;
        }


        switchCamera.SwitchCameras();
    }
}
