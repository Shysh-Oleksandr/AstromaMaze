using UnityEngine;

public class Paint : MonoBehaviour
{
    public LayerMask paintableLayer;
    public Transform playerTransform;
    public SprayItem sprayItem;

    [SerializeField] private float brushSize = 0.1f, maxDistance = 10f;
    [SerializeField] private float localRotationY;

    public delegate void OnSprayChanged();

    public event OnSprayChanged OnSprayChangedEvent;

    private ObjectPooler objectPooler;
    private AudioSource spraySource;

    Camera mainCam;
    void Start()
    {
        foreach (Sound s in AudioManager.Instance.sounds)
        {
            if (s.name == "Spray")
            {
                spraySource = s.source;
            }
        }

        objectPooler = ObjectPooler.instance;
        mainCam = Camera.main;

        brushSize = sprayItem.brushSize;
        maxDistance = sprayItem.maxDistance;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && 
            mainCam.gameObject.activeSelf && 
            GameManager.Instance.isGameRunning &&
            objectPooler.sprayAmount > 0)
        {
            PaintWall();
        }
        else
        {
            spraySource.Stop();
        }
    }

    private void PaintWall()
    {
        var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(Ray, out hit, maxDistance, paintableLayer))
        {
            var paint = objectPooler.SpawnFromPool("Paint", hit.point + Vector3.one * 0.1f, Quaternion.identity);

            if (paint != null)
            {
                if (hit.collider.gameObject.CompareTag("Wall") || hit.collider.gameObject.CompareTag("SurprisedWall"))
                {
                    Vector3 positionOuter = hit.point - Vector3.one * 0.1f;
                    Vector3 positionInner = hit.point + Vector3.one * 0.1f;
                    Quaternion rotationOuter = Quaternion.Euler(-90f, 0, 0);
                    Quaternion rotationInner = Quaternion.Euler(-90f, 0, -90f);

                    localRotationY = FindLocalRotationY();

                    if (localRotationY == 0 || localRotationY == 360f)
                    {
                        paint = objectPooler.SpawnFromPool("Paint", positionOuter, rotationOuter);
                    }
                    else if (localRotationY == 180f)
                    {
                        paint = objectPooler.SpawnFromPool("Paint", positionInner, rotationOuter);

                    }
                    else if (localRotationY == 90f)
                    {

                        paint = objectPooler.SpawnFromPool("Paint", positionOuter, rotationInner);

                    }
                    else if (localRotationY == 270f)
                    {
                        paint = objectPooler.SpawnFromPool("Paint", positionInner, rotationInner);
                    }
                }
                else
                {
                    Vector3 position = hit.point + Vector3.one * 0.1f;
                    paint = objectPooler.SpawnFromPool("Paint", position, Quaternion.identity);

                }
                if (!spraySource.isPlaying)
                {
                    AudioManager.Instance.Play("Spray");
                }
                paint.transform.localScale = Vector3.one * brushSize;
                paint.SetActive(true);
                IPooledObject pooledObj = paint.GetComponent<IPooledObject>();

                if (pooledObj != null)
                {
                    pooledObj.OnObjectSpawn();
                }

                objectPooler.sprayAmount--;
                OnSprayChangedEvent?.Invoke();
            }
        }
        else
        {
            spraySource.Stop();
        }
    }

    private float FindLocalRotationY()
    {
        localRotationY = playerTransform.eulerAngles.y;
        localRotationY = Mathf.RoundToInt(localRotationY / 90) * 90;

        return localRotationY;
    }
}
