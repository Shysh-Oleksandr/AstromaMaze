using System.Collections;
using UnityEngine;

public class BirdsEyeView : MonoBehaviour
{
    public AnimationCurve upEasing, downEasing;

    public Camera mainCam;
    public Camera birdsEyeCam;
    public Transform playerTransform;
    public SwitchCamera switchCamera;
    public SpellCooldown spellCooldown;
    public BirdsEyeViewItem birdsEyeViewItem;

    public delegate void OnBirdsEyeView();
    public event OnBirdsEyeView OnBirdsEyeViewEvent;

    [SerializeField] [Range(0, 5)] private float startOffset;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        OnBirdsEyeViewEvent += spellCooldown.UseSpell;
    }

    private void OnEnable()
    {
        startPosition = transform.position;

        StartCoroutine(BirdsEyeViewCoroutine());
    }

    private void OnDestroy()
    {
        OnBirdsEyeViewEvent -= spellCooldown.UseSpell;
    }

    IEnumerator BirdsEyeViewCoroutine()
    {
        AudioManager.Instance.Play("BirdsUp");

        for (float i = 0; i < 1; i += Time.deltaTime / birdsEyeViewItem.coroutineDuration)
        {
            targetPosition = playerTransform.position + Vector3.up * birdsEyeViewItem.maxHeight;
            transform.position = Vector3.Lerp(startPosition, targetPosition, upEasing.Evaluate(i));
            yield return null;
        }

        yield return new WaitForSeconds(birdsEyeViewItem.maxHeightDuration);
        AudioManager.Instance.Play("BirdsDown");

        startPosition = transform.position;
        targetPosition = playerTransform.position;

        for (float i = 0; i < 1; i += Time.deltaTime / birdsEyeViewItem.coroutineDuration)
        {
            targetPosition = playerTransform.position + Vector3.up * startOffset;
            transform.position = Vector3.Lerp(startPosition, targetPosition, downEasing.Evaluate(i));
            yield return null;
        }

        var tempColor = spellCooldown.skillImage.color;
        tempColor.a = 0.8f;
        spellCooldown.skillImage.color = tempColor;
        OnBirdsEyeViewEvent?.Invoke();
        switchCamera.SwitchCameras();
    }
}
