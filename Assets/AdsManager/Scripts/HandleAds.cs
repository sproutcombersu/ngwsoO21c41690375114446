using System;
using System.Collections.Generic;

public class HandleAds {
    private AdmobAds admobAds;
    private FanAds fanAds;

    private ApplovinMaxAds applovinMaxAds;
    private ApplovinDiscoveryAds applovinDiscoveryAds;
    private UnityAdsNetwork unityAds;

    public HandleAds (AdmobAds admobAds, FanAds fanAds, ApplovinMaxAds applovinMaxAds, ApplovinDiscoveryAds applovinDiscoveryAds, UnityAdsNetwork unityAdsNetwork) {
        this.admobAds = admobAds;
        this.fanAds = fanAds;
        this.applovinMaxAds = applovinMaxAds;
        this.applovinDiscoveryAds = applovinDiscoveryAds;
        this.unityAds = unityAdsNetwork;
    }

    [Obsolete]
    public void Initialize(string appId, Action<IInitialize> iInitialize, List<string> testDevices, NetworkAds networkAds)
    {
        switch (networkAds)
        {
            case NetworkAds.ADMOB:
                admobAds.Initialize(appId, iInitialize, testDevices);
                break;
            case NetworkAds.FAN:
                fanAds.Initialize(appId, iInitialize, testDevices);
                break;
            case NetworkAds.APPLOVIN_MAX:
                applovinMaxAds.Initialize(appId, iInitialize, testDevices);
                break;
            case NetworkAds.APPLOVIN_DISCOVERY:
                applovinDiscoveryAds.Initialize(appId, iInitialize, testDevices);
                break;
            case NetworkAds.UNITY_ADS:
                unityAds.Initialize(appId, iInitialize, testDevices);
                break;
            default:
                // Do nothing
                break;
        }
    }

    [Obsolete]
    public void ShowBanner(NetworkAds networkAds, string adUnitId,  Action<string> onAdLoded, Action<string> onAdFailedToLoad, AdsBannerPosition adsBannerPosition = AdsBannerPosition.BOTTOM_CENTER) {
        if(string.IsNullOrEmpty(adUnitId)) {
            onAdFailedToLoad("adUnit Empty");

            return;
        }
        switch (networkAds)
        {
            case NetworkAds.ADMOB:
                admobAds.ShowBanner(adUnitId, onAdLoded, onAdFailedToLoad, adsBannerPosition);
                break;
            case NetworkAds.FAN:
                fanAds.ShowBanner(adUnitId, onAdLoded, onAdFailedToLoad, adsBannerPosition);
                break;
            case NetworkAds.APPLOVIN_MAX:
                applovinMaxAds.ShowBanner(adUnitId, onAdLoded, onAdFailedToLoad, adsBannerPosition);
                break;
            case NetworkAds.APPLOVIN_DISCOVERY:
                applovinDiscoveryAds.ShowBanner(adUnitId, onAdLoded, onAdFailedToLoad, adsBannerPosition);
                break;
            case NetworkAds.UNITY_ADS:
                unityAds.ShowBanner(adUnitId, onAdLoded, onAdFailedToLoad, adsBannerPosition);
                break;
            default:
                onAdFailedToLoad("network none");
                break;
        }
    }

     public void HideBanner(NetworkAds networkAds) {
        
        switch (networkAds)
        {
            case NetworkAds.ADMOB:
                admobAds.HideBanner();
                break;
            case NetworkAds.FAN:
                fanAds.HideBanner();
                break;
            case NetworkAds.APPLOVIN_MAX:
                applovinMaxAds.HideBanner();
                break;
            case NetworkAds.APPLOVIN_DISCOVERY:
                applovinDiscoveryAds.HideBanner();
                break;
            case NetworkAds.UNITY_ADS:
                unityAds.HideBanner();
                break;
            default:
                // Do nothing
                break;
        }
    }

    [Obsolete]
    public void LoadInterstitial(NetworkAds networkAds, string adUnitId) {
        
        if (string.IsNullOrEmpty(adUnitId))
        { 
            return;
        }
        switch (networkAds)
        {
            case NetworkAds.ADMOB:
                admobAds.LoadInterstitial(adUnitId);
                break;
            case NetworkAds.FAN:
                fanAds.LoadInterstitial(adUnitId);
                break;
            case NetworkAds.APPLOVIN_MAX:
                applovinMaxAds.LoadInterstitial(adUnitId);
                break;
            case NetworkAds.APPLOVIN_DISCOVERY:
                applovinDiscoveryAds.LoadInterstitial(adUnitId);
                break;
            case NetworkAds.UNITY_ADS:
                unityAds.LoadInterstitial(adUnitId);
                break;
            default:
                // Do nothing
                break;
        }
    }

    [Obsolete]
    public void ShowInterstitial(
        NetworkAds networkAds,
        string adUnitId,
        Action<string> onAdLoded, Action<string> onAdFailedToLoad)
    {
        if(string.IsNullOrEmpty(adUnitId)) {
            onAdFailedToLoad("adUnit Empty");
            return;
        }

        switch (networkAds)
        {
            case NetworkAds.ADMOB:
                admobAds.ShowInterstitial( adUnitId, onAdLoded, onAdFailedToLoad);
                break;
            case NetworkAds.FAN:
                fanAds.ShowInterstitial( adUnitId, onAdLoded, onAdFailedToLoad);
                break;
            case NetworkAds.APPLOVIN_MAX:
                applovinMaxAds.ShowInterstitial( adUnitId, onAdLoded, onAdFailedToLoad);
                break;
            case NetworkAds.APPLOVIN_DISCOVERY:
                applovinDiscoveryAds.ShowInterstitial( adUnitId, onAdLoded, onAdFailedToLoad);
                break;
            case NetworkAds.UNITY_ADS:
                unityAds.ShowInterstitial(adUnitId, onAdLoded, onAdFailedToLoad);
                break;
            default:
                onAdFailedToLoad("network none");
                break;
        }
    }

    public void ShowNativeAds(
       
        NetworkAds networkAds,
        string adUnitId,
        Action<CallbackAds> callbackAds)
    {
        var cb = new CallbackAds();
        if(string.IsNullOrEmpty(adUnitId)) {
            cb.OnAdFailedToLoad("adUnit Empty");
            callbackAds(cb);
            return;
        }

        switch (networkAds)
        {
            case NetworkAds.ADMOB:
                admobAds.ShowNativeAds( adUnitId, callbackAds);
                break;
            case NetworkAds.FAN:
                fanAds.ShowNativeAds( adUnitId, callbackAds);
                break;
            case NetworkAds.APPLOVIN_MAX:
                applovinMaxAds.ShowNativeAds( adUnitId, callbackAds);
                break;
            case NetworkAds.APPLOVIN_DISCOVERY:
                applovinDiscoveryAds.ShowNativeAds( adUnitId, callbackAds);
                break;
            default:
                cb.OnAdFailedToLoad("network none");
                callbackAds(cb);
                break;
        }
    }

    public void LoadRewards(
        
        NetworkAds networkAds,
        string adUnitId)
    {
        if (string.IsNullOrEmpty(adUnitId))
        { 
            return;
        }

        switch (networkAds)
        {
            case NetworkAds.ADMOB:
                admobAds.LoadRewards( adUnitId);
                break;
            case NetworkAds.FAN:
                fanAds.LoadRewards( adUnitId);
                break;
            case NetworkAds.APPLOVIN_MAX:
                applovinMaxAds.LoadRewards( adUnitId);
                break;
            case NetworkAds.APPLOVIN_DISCOVERY:
                applovinDiscoveryAds.LoadRewards( adUnitId);
                break;
            case NetworkAds.UNITY_ADS:
                unityAds.LoadRewards( adUnitId);
                break;
            default:
                break;
        }
    }

    public void ShowRewards(
        
        NetworkAds networkAds,
        string adUnitId,
        Action<string> onAdLoded, Action<string> onAdFailedToLoad,
        Action<IRewards> iRewards)
    {
        if(string.IsNullOrEmpty(adUnitId)) {
            onAdFailedToLoad("adUnit Empty");
            return;
        }

        switch (networkAds)
        {
            case NetworkAds.ADMOB:
                admobAds.ShowRewards( adUnitId, onAdLoded, onAdFailedToLoad, iRewards);
                break;
            case NetworkAds.FAN:
                fanAds.ShowRewards( adUnitId, onAdLoded, onAdFailedToLoad, iRewards);
                break;
            case NetworkAds.APPLOVIN_MAX:
                applovinMaxAds.ShowRewards( adUnitId, onAdLoded, onAdFailedToLoad, iRewards);
                break;
            case NetworkAds.APPLOVIN_DISCOVERY:
                applovinDiscoveryAds.ShowRewards( adUnitId, onAdLoded, onAdFailedToLoad, iRewards);
                break;
            case NetworkAds.UNITY_ADS:
                unityAds.ShowRewards( adUnitId, onAdLoded, onAdFailedToLoad, iRewards);
                break;
            default:
                onAdFailedToLoad("network none");
                break;
        }
    }
}