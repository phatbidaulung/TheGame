using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GoogleMobileAds.Api;

using Ensign.Unity;

public class GoogleAdMobController : Singleton<GoogleAdMobController> {

    private InterstitialAd _interstitial_Ad;
    private RewardedAd _rewardedAd;
    private BannerView _bannerView;

    private string _interstitial_Ad_ID;
    private string _rewardedAd_ID;

    public bool useBanner = false;

    private void Start() 
    {
        _interstitial_Ad_ID = "ca-app-pub-3940256099942544/1033173712";
        _rewardedAd_ID = "ca-app-pub-3940256099942544/5224354917";

        MobileAds.Initialize (initStatus => { });

        RequestInterstitial();
        RequestRewardedVideo();
        if(!IngameData.Instance.IsShowAds)
            RequestBanner();

        DontDestroyOnLoad(this.gameObject);
    }
    

    private void RequestInterstitial() 
    {
        _interstitial_Ad = new InterstitialAd(_interstitial_Ad_ID);
        _interstitial_Ad.OnAdLoaded += HandleOnAdLoaded;
        AdRequest request = new AdRequest.Builder ().Build ();
        _interstitial_Ad.LoadAd (request);
    }

    private void RequestRewardedVideo() 
    {
        _rewardedAd = new RewardedAd (_rewardedAd_ID);
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        AdRequest request = new AdRequest.Builder ().Build ();
        _rewardedAd.LoadAd (request);
    }

    private void RequestBanner()
    {
        // Create a 320x50 banner at the top of the screen.
        _bannerView = new BannerView(_interstitial_Ad_ID, AdSize.SmartBanner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        _bannerView.LoadAd(request);
    }

    public void ShowBanner(bool show)
    {
        if (_bannerView != null)
        {
            if (useBanner)
            {
#if UNITY_ANDROID || UNITY_IOS
                if (show)
                    _bannerView.Show();
                else
                    _bannerView.Hide();
#endif
            }
        }
    }
    ///<summary>
    /// Show the rewarded ad
    ///</summary>
    public void ShowInterstitial() 
    {
        if (_interstitial_Ad.IsLoaded()) 
        {
            _interstitial_Ad.Show ();
            RequestInterstitial ();
        }

    }

    ///<summary>
    /// Show the rewarded interstitial ad
    ///</summary>
    public void ShowRewardedVideo()
    {
        if (_rewardedAd.IsLoaded ()) 
        {
            _rewardedAd.Show ();
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args) 
    {

    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args) 
    {
        RequestRewardedVideo();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args) 
    {
        RequestRewardedVideo();
    }

    public void HandleUserEarnedReward(object sender, Reward args) 
    {
        RequestRewardedVideo();
    }
}