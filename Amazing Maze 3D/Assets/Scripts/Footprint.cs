using UnityEngine;

public class Footprint : MonoBehaviour
{

    [SerializeField] private float footprintOffset = 0.05f;
    [SerializeField] private float footprintIntervalDistance = 3f;

    public GameObject leftFootprint, rightFootprint;
    public GameObject player;
    public Transform leftFootLocation, rightFootLocation;

    private bool isRightFootprint = false;
    private Vector3 lastFootprint;

    private void Update()
    {
        if (!isRightFootprint)
        {
            RightFootprint();
        }
        else
        {
            LeftFootprint();
        }
    }

    void LeftFootprint()
    {
        RaycastHit hit;

        if (Physics.Raycast(leftFootLocation.position, leftFootLocation.forward, out hit))
        {
            Vector3 position = hit.point + hit.normal * footprintOffset;
            Quaternion rotation = Quaternion.LookRotation(hit.normal, leftFootLocation.up);

            if (Vector3.Distance(lastFootprint, position) > footprintIntervalDistance)
            {
                Instantiate(leftFootprint, position, rotation);
                lastFootprint = position;
                isRightFootprint = false;
            }
        }
    }

    void RightFootprint()
    {
        RaycastHit hit;

        if (Physics.Raycast(rightFootLocation.position, rightFootLocation.forward, out hit))
        {
            Vector3 position = hit.point + hit.normal * footprintOffset;
            Quaternion rotation = Quaternion.LookRotation(hit.normal, rightFootLocation.up);

            if (Vector3.Distance(lastFootprint, position) > footprintIntervalDistance)
            {
                Instantiate(rightFootprint, position, rotation);
                lastFootprint = position;
                isRightFootprint = true;
            }
        }
    }


}
