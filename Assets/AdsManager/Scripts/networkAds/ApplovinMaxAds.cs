using System;
using System.Collections.Generic;
using UnityEngine;

public class ApplovinMaxAds : MonoBehaviour, IAds
{
    int retryAttempt;
    int retryAttemptRewards;
    Action<IRewards> iRewards;

    public void Initialize(string appId, Action<IInitialize> iInitialize, List<string> testDevices)
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (
            MaxSdkBase.SdkConfiguration sdkConfiguration
        ) =>
        {
            // AppLovin SDK is initialized, start loading ads
        };

        var sdkKey = appId;
        if (!string.IsNullOrEmpty(EaAds.GlobalValue.applovinSdkKey))
        {
            sdkKey = EaAds.GlobalValue.applovinSdkKey;
        }
        Debug.Log("sdk Key bro: " + sdkKey);
        MaxSdk.SetSdkKey(sdkKey);
        MaxSdk.InitializeSdk();
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
    }

    public void LoadGdpr(bool childDirected) { }

    public void LoadInterstitial(string adUnitId)
    {
        MaxSdk.LoadInterstitial(adUnitId);
    }

    public void LoadRewards(string adUnitId)
    {
        MaxSdk.LoadRewardedAd(adUnitId);
    }

    private string adUnitBannerId = "sdfsdfs";
    public void HideBanner()
    {
        MaxSdk.HideBanner(adUnitBannerId);
    }

    public void ShowBanner(
        string adUnitId,
         Action<string> onAdLoded, Action<string> onAdFailedToLoad,
        AdsBannerPosition adsBannerPosition
    )
    {
        adUnitBannerId = adUnitId;
        try
        {
            // Kode yang mungkin memunculkan exception

            // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
            // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments

            var posisition = MaxSdkBase.BannerPosition.BottomCenter;
            if (adsBannerPosition == AdsBannerPosition.TOP_CENTER)
            {
                posisition = MaxSdkBase.BannerPosition.TopCenter;
            }
            MaxSdk.CreateBanner(adUnitId, posisition);

            MaxSdkCallbacks.Banner.OnAdLoadedEvent += (string adUnitId, MaxSdkBase.AdInfo adInfo) =>
            {
                onAdLoded("loadedBannerApplovin");
            };
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += (
                string adUnitId,
                MaxSdkBase.ErrorInfo errorInfo
            ) =>
            {
                onAdFailedToLoad(errorInfo.Message);
            };
            MaxSdk.ShowBanner(adUnitId);
        }
        catch (System.DivideByZeroException e)
        {
            // Exception yang dihasilkan oleh pembagian dengan nol

            onAdFailedToLoad(e.Message);
        }
        catch (System.Exception e)
        {
            // Exception umum lainnya
            onAdFailedToLoad(e.Message);
        }
        finally
        {
            // Blok finally akan selalu dijalankan
        }
    }

    public void ShowInterstitial(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad)
    {
        if (MaxSdk.IsInterstitialReady(adUnitId))
        {
            MaxSdk.ShowInterstitial(adUnitId);
            onAdLoded.Invoke("applovin int show");
        }
        else
        {
            onAdFailedToLoad.Invoke("applovin int failed to show");
        }
    }

    public void ShowNativeAds(string adUnitId, Action<CallbackAds> callbackAds) { }

    public void ShowRewards(
        string adUnitId,
         Action<string> onAdLoded, Action<string> onAdFailedToLoad,
        Action<IRewards> iRewards
    )
    {
        var cb = new CallbackAds();
        this.iRewards = iRewards;
        if (MaxSdk.IsRewardedAdReady(adUnitId))
        {
            MaxSdk.ShowRewardedAd(adUnitId);
            onAdLoded("show rewards Applovin");
        }
        else
        {
            onAdFailedToLoad("failed show rewards applovin");
        }
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        retryAttempt = 0;
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialAdFailedToDisplayEvent(
        string adUnitId,
        MaxSdkBase.ErrorInfo errorInfo,
        MaxSdkBase.AdInfo adInfo
    )
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterstitial(adUnitId);
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        LoadInterstitial(adUnitId);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        retryAttemptRewards = 0;
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        retryAttemptRewards++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttemptRewards));

        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnRewardedAdFailedToDisplayEvent(
        string adUnitId,
        MaxSdkBase.ErrorInfo errorInfo,
        MaxSdkBase.AdInfo adInfo
    )
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        LoadRewards(adUnitId);
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewards(adUnitId);
    }

    private void OnRewardedAdReceivedRewardEvent(
        string adUnitId,
        MaxSdk.Reward reward,
        MaxSdkBase.AdInfo adInfo
    )
    {
        // The rewarded ad displayed and the user should receive the reward.
        var _iRewards = new IRewards();
        var rewardsItem = new MyRewardsItem();
        rewardsItem.Amount = 10;
        rewardsItem.Type = "rewards";
        _iRewards.OnUserEarnedReward(rewardsItem);
        iRewards(_iRewards);
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Ad revenue paid. Use this callback to track user revenue.
    }
}
