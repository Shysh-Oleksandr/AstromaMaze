using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform mPlayerTransform;
    [SerializeField] float rotationSpeed;

    [Tooltip("The direction towards which the compass points. Default for North is (0, 0, 1)")]
    public Vector3 kReferenceVector = new Vector3(0, 0, 1);

    private Vector3 _mTempVector;
    private float _mTempAngle;


    private void Update()
    {
        CompassRotation();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R");
            // Determine which direction to rotate towards
            Vector3 targetDirection = Vector3.left - mPlayerTransform.position;

            // The step size is equal to speed times frame time.
            float singleStep = rotationSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(mPlayerTransform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(mPlayerTransform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            mPlayerTransform.rotation = Quaternion.LookRotation(newDirection);
        }

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
