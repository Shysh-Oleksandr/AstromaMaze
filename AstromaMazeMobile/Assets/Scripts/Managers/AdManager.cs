using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;   

public class AdManager : MonoBehaviour
{
    [SerializeField] private bool _testMode = true;

    private readonly string _gameId = "4373219";

    public static readonly string _video = "Interstitial_Android";

    public static int numToNextAd;

    private void Start()
    {
        numToNextAd = PlayerPrefs.GetInt("numToAd", 0);
        Advertisement.Initialize(_gameId, _testMode);
    }

    public static void PlayAd(string placementId)
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(placementId);
        }
    }
}
