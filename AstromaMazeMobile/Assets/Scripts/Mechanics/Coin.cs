using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue;

    private void Start()
    {
        coinValue = Random.Range(2, 6);
    }
}
