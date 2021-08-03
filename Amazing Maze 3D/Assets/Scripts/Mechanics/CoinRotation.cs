using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    [SerializeField] int rotateSpeed;

    [Range(1, 3)]private int axis;

    private void Start()
    {
        rotateSpeed = Random.Range(90, 221);

        axis = Random.Range(1, 4);
    }

    void Update()
    {
        switch (axis)
        {
            case 1:
                transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0);
                break;
            case 2:
                transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
                break;
            case 3:
                transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
                break;
            
        }
    }
}
