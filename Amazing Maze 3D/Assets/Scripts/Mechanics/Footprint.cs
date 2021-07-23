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
    private ObjectPooler objectPooler;


    private void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

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
                AudioManager.Instance.Play("Footstep");
                var leftFootprint = objectPooler.SpawnFromPool("LeftFootprint", position, rotation);
                leftFootprint.SetActive(true);
                LaunchOnSpawn(leftFootprint);

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
                AudioManager.Instance.Play("Footstep");

                var rightFootprint = objectPooler.SpawnFromPool("RightFootprint", position, rotation);
                rightFootprint.SetActive(true);
                LaunchOnSpawn(rightFootprint);
                lastFootprint = position;
                isRightFootprint = true;
            }
        }
    }

    private static void LaunchOnSpawn(GameObject footprint)
    {
        IPooledObject pooledObj = footprint.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
    }
}
