using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    [SerializeField] int rotateSpeedY, rotateSpeedX, rotateSpeedZ;

    void Update()
    {
        transform.Rotate(rotateSpeedX * Time.deltaTime, rotateSpeedY * Time.deltaTime, rotateSpeedZ * Time.deltaTime);
    }
}
