using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class HomeAdController : MonoBehaviour
{
    private BannerView bannerView;
    private void OnEnable()
    {
        this.RequestBanner();
    }

    private void OnDisable()
    {
        bannerView.Destroy();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7883850082959526/1111765919";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-4502665724831009~7385938371";
#else
            string adUnitId = "unexpected_platform";
#endif
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        print("Ad successfully loaded!");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("Ad failed to load with message: " + args.Message);
    }
}
