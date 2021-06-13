using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI sprayText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sprayText.text = "Spray: " + ObjectPooler.instance.sprayAmount;
    }
}
