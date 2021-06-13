using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    public LayerMask paintableLayer;
    public Transform playerTransform;

    [SerializeField] private float brushSize = 0.1f, availableDistance= 15f;
    public float distanceToHit, localRotationY;

    Camera mainCam;
    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && mainCam.gameObject.activeSelf)
        {
            Paint();
        }
    }

    private void Paint()
    {
        var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(Ray, out hit, paintableLayer))
        {
            distanceToHit = Vector3.Distance(playerTransform.position, hit.point);
            if (distanceToHit < availableDistance)
            {
                var paint = ObjectPooler.instance.GetPooledObject();
                if (paint != null)
                {
                    localRotationY = playerTransform.eulerAngles.y;
                    localRotationY = Mathf.RoundToInt(localRotationY / 90) * 90;

                    if (hit.collider.gameObject.CompareTag("Wall"))
                    {
                        if (localRotationY == 0 || localRotationY == 360f)
                        {
                            paint.transform.rotation = Quaternion.Euler(-90f, 0, 0);
                            paint.transform.position = hit.point - Vector3.one * 0.1f;
                        }
                        else if (localRotationY == 180f)
                        {
                            paint.transform.rotation = Quaternion.Euler(-90f, 0, 0);
                            paint.transform.position = hit.point + Vector3.one * 0.1f;
                        }
                        else if (localRotationY == 90f)
                        {
                            paint.transform.rotation = Quaternion.Euler(-90f, 0, -90f);
                            paint.transform.position = hit.point - Vector3.one * 0.1f;
                        }
                        else if (localRotationY == 270f)
                        {
                            paint.transform.rotation = Quaternion.Euler(-90f, 0, -90f);
                            paint.transform.position = hit.point + Vector3.one * 0.1f;
                        } 
                    }
                    else
                    {
                        paint.transform.rotation = Quaternion.identity;
                        paint.transform.position = hit.point + Vector3.one * 0.1f;
                    }

                    paint.transform.localScale = Vector3.one * brushSize;
                    paint.SetActive(true);
                    ObjectPooler.instance.sprayAmount--;
                }
            }
        }
    }
}
