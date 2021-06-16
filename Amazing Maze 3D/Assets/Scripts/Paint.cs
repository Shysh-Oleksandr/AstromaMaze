using UnityEngine;

public class Paint : MonoBehaviour
{
    public LayerMask paintableLayer;
    public Transform playerTransform;

    [SerializeField] private float brushSize = 0.1f, availableDistance = 10f;
    public float distanceToHit, localRotationY;

    private ObjectPooler objectPooler;

    Camera mainCam;
    void Start()
    {
        objectPooler = ObjectPooler.instance;
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && mainCam.gameObject.activeSelf && GameManager.isGameRunning)
        {
            PaintWall();
        }
    }

    private void PaintWall()
    {
        var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(Ray, out hit, paintableLayer))
        {
            distanceToHit = Vector3.Distance(playerTransform.position, hit.point);
            if (distanceToHit < availableDistance)
            {
                var paint = objectPooler.SpawnFromPool("Paint", hit.point + Vector3.one * 0.1f, Quaternion.identity);

                if(paint != null)
                {
                    if (hit.collider.gameObject.CompareTag("Wall") || hit.collider.gameObject.CompareTag("Door"))
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

                    paint.transform.localScale = Vector3.one * brushSize;
                    paint.SetActive(true);
                    objectPooler.sprayAmount--;
                }
            }
        }
    }

    private float FindLocalRotationY()
    {
        localRotationY = playerTransform.eulerAngles.y;
        localRotationY = Mathf.RoundToInt(localRotationY / 90) * 90;

        return localRotationY;
    }
}
