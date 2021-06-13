using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    public LayerMask paintableLayer;
    public Transform playerTransform;

    [SerializeField] private float brushSize = 0.1f, availableDistance= 15f;
    private float distanceToHit;

    Camera mainCam;
    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && mainCam.gameObject.activeSelf)
        {
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(Ray, out hit, paintableLayer))
            {
                distanceToHit = Vector3.Distance(playerTransform.position, hit.point);
                if(distanceToHit < availableDistance)
                {
                    var paint = ObjectPooler.instance.GetPooledObject();
                    if (paint != null)
                    {
                        paint.transform.position = hit.point + Vector3.one * 0.1f;
                        paint.transform.rotation = Quaternion.identity;
                        paint.transform.localScale = Vector3.one * brushSize;
                        paint.SetActive(true);
                    }
                }
            }
        }
    }
}
