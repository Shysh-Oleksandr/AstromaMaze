using System.Collections;
using UnityEngine;

public class BirdsEyeView : MonoBehaviour
{
    public AnimationCurve upEasing, downEasing;

    public Camera mainCam;
    public Camera birdsEyeCam;
    public Transform playerTransform;
    public SwitchCamera switchCamera;
    public BirdsEyeViewItem birdsEyeViewItem;

    [SerializeField] [Range(0, 5)] private float startOffset;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void OnEnable()
    {
        startPosition = transform.position;

        StartCoroutine(BirdsEyeViewCoroutine());
    }


    IEnumerator BirdsEyeViewCoroutine()
    {
        for (float i = 0; i < 1; i += Time.deltaTime / birdsEyeViewItem.coroutineDuration)
        {
            targetPosition = playerTransform.position + Vector3.up * birdsEyeViewItem.maxHeight;
            transform.position = Vector3.Lerp(startPosition, targetPosition, upEasing.Evaluate(i));
            yield return null;
        }

        yield return new WaitForSeconds(birdsEyeViewItem.maxHeightDuration);

        startPosition = transform.position;
        targetPosition = playerTransform.position;

        for (float i = 0; i < 1; i += Time.deltaTime / birdsEyeViewItem.coroutineDuration)
        {
            targetPosition = playerTransform.position + Vector3.up * startOffset;
            transform.position = Vector3.Lerp(startPosition, targetPosition, downEasing.Evaluate(i));
            yield return null;
        }

        switchCamera.nextBirdsEyeView = Time.time + birdsEyeViewItem.birdsEyeViewCooldown;
        switchCamera.SwitchCameras();
    }
}
