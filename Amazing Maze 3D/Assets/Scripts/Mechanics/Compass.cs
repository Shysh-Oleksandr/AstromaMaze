using System.Collections;
using UnityEngine;
using TMPro;

public class Compass : MonoBehaviour
{
    public Transform mPlayerTransform, exitTransform;
    public GameObject compassPanel;
    public CompassItem compassItem;
    public TextMeshProUGUI distanceText;
    [SerializeField] float rotationSpeed, distanceToExit;

    [Tooltip("The direction towards which the compass points. Default for North is (0, 0, 1)")]
    public Vector3 kReferenceVector = new Vector3(0, 0, 1);

    private Vector3 _mTempVector;
    private float _mTempAngle;
    private float nextRotateToNorth;

    private void Start()
    {
        compassPanel.SetActive(compassItem.isBought);
        distanceText.gameObject.SetActive(compassItem.canShowDistance);
    }

    private void Update()
    {
        CompassRotation();

        if (Input.GetKeyDown(KeyCode.R) && compassItem.canRotateToNorth && Time.time > nextRotateToNorth)
        {
            nextRotateToNorth = Time.time + compassItem.compassCooldown;
            StartCoroutine(RotateToNorth());
        }

        if (compassItem.canShowDistance)
        {
            distanceToExit = Vector3.Distance(mPlayerTransform.position, exitTransform.position);
            distanceText.text = (int)distanceToExit + " m to exit";
        }

    }

    IEnumerator RotateToNorth()
    {
        Quaternion targetRotation = Quaternion.identity;
        do
        {
            targetRotation = Quaternion.LookRotation(Vector3.left);
            mPlayerTransform.rotation = Quaternion.RotateTowards(mPlayerTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null;

        } while (Quaternion.Angle(mPlayerTransform.rotation, targetRotation) > 0.01f);
    }

    private void CompassRotation()
    {
        // get player transform, set y to 0 and normalize
        _mTempVector = mPlayerTransform.forward;
        _mTempVector.y = 0f;
        _mTempVector = _mTempVector.normalized;

        // get distance to reference, ensure y equals 0 and normalize
        _mTempVector = _mTempVector - kReferenceVector;
        _mTempVector.y = 0;
        _mTempVector = _mTempVector.normalized;

        // if the distance between the two vectors is 0, this causes an issue with angle computation afterwards  
        if (_mTempVector == Vector3.zero)
        {
            _mTempVector = new Vector3(1, 0, 0);
        }

        // compute the rotation angle in radians and adjust it 
        _mTempAngle = Mathf.Atan2(_mTempVector.x, _mTempVector.z);
        _mTempAngle = (_mTempAngle * Mathf.Rad2Deg - 45f) * 2f;

        // set rotation
        transform.rotation = Quaternion.AngleAxis(_mTempAngle, kReferenceVector);
    }
}
