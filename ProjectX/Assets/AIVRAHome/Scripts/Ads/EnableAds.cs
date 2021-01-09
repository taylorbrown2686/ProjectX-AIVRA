using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class EnableAds : MonoBehaviour
{
    private void Start()
    {
        //if NO ADS has not been purchased (set up IAP)
        MobileAds.Initialize(initStatus => { });
    }
}
