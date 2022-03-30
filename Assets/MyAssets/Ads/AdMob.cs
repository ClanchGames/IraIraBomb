using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;


public class AdMob : MonoBehaviour
{
    public static AdMob adMob;
    public string appId;
    public string bannerId;
    public string largeBannerId;
    public string interstitialId;
    public string rewardId;

    private BannerView bannerView;
    private BannerView largeBannerView;

    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private bool isRewarded = false;

    string testdeviceID = "353001080687321";
    // Use this for initialization
    void Start()
    {
        adMob = this;

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
        RequestReward();
        RequestInterstitial();

    }
    private void Update()
    {
        if (isRewarded)
        {
            isRewarded = false;
            GetReward();
        }
    }
    public void Test()
    {
        Debug.Log("test");
    }

    public void RequestBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        bannerView = new BannerView(bannerId, adaptiveSize, AdPosition.Bottom);

        bannerView.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs args) =>
        {
            bannerView.Destroy();
        };

        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);

        Debug.Log("request banner");

    }
    public void HideBanner()
    {
        if (bannerView != null)
        {
            Debug.Log("hidebanner");
            bannerView.Hide();
            bannerView.Destroy();
            bannerView = null;
        }
    }
    public void ShowBanner()
    {
        Debug.Log("showbanner");
        if (bannerView == null)
        {
            Debug.Log("bannerview null request");
            RequestBanner();
        }
        else
        {
            Debug.Log("bannerview not null show");
            bannerView.Show();
        }
    }

    public void RequestLargeBanner()
    {
        if (largeBannerView != null)
        {
            largeBannerView.Destroy();
        }

        largeBannerView = new BannerView(largeBannerId, AdSize.MediumRectangle, AdPosition.Center);

        largeBannerView.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs args) =>
        {
            largeBannerView.Destroy();
        };

        AdRequest request = new AdRequest.Builder().Build();
        largeBannerView.LoadAd(request);
    }

    public void HideLargeBanner()
    {
        if (largeBannerView != null)
        {
            Debug.Log("hide large");
            largeBannerView.Hide();
            largeBannerView.Destroy();
            largeBannerView = null;
        }
    }

    public void RequestInterstitial()
    {
        interstitial = new InterstitialAd(interstitialId);

        interstitial.OnAdLoaded += HandleInterstitialAdLoaded;
        interstitial.OnAdFailedToLoad += HandleInterstitialAdFailedToLoad;
        interstitial.OnAdOpening += HandleInterstitialAdOpening;
        interstitial.OnAdClosed += HandleInterstitialAdClosed;
        interstitial.OnAdLeavingApplication += HandleInterstitialAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().AddTestDevice(testdeviceID).Build();
        interstitial.LoadAd(request);
    }
    public void ShowInterstitial()
    {
        if (!interstitial.IsLoaded())
        {
            RequestInterstitial();
        }
        interstitial.Show();
    }
    public void HandleInterstitialAdLoaded(object sender, EventArgs args)
    {

    }
    public void HandleInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }
    public void HandleInterstitialAdOpening(object sender, EventArgs args)
    {

    }
    public void HandleInterstitialAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
        Debug.Log("interstitial close");
    }
    public void HandleInterstitialAdLeavingApplication(object sender, EventArgs args)
    {

    }

    public void RequestReward()
    {
        rewardedAd = new RewardedAd(rewardId);

        // Load�������Ɏ��s����֐��̓o�^
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Load���s���Ɏ��s����֐��̓o�^
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // �\�����Ɏ��s����֐��̓o�^
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // �\�����s���Ɏ��s����֐��̓o�^
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // ��V�󂯎�莞�Ɏ��s����֐��̓o�^
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // �L������鎞�Ɏ��s����֐��̓o�^
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }
    public void ShowRewardAd()
    {
        if (!rewardedAd.IsLoaded())
        {
            RequestReward();
        }
        rewardedAd.Show();
    }
    public void GetReward()
    {
        Debug.Log("Show Reward");
    }

    public void CreateAndLoadRewardedAd()
    {
        rewardedAd = new RewardedAd(rewardId);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().AddTestDevice(testdeviceID).Build();
        rewardedAd.LoadAd(request);
    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);

    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        print("HandleRewardedAdClosed event received");
        CreateAndLoadRewardedAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        isRewarded = true;
    }
}
