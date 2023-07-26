using System;
using System.Collections.Generic;

public class ApplovinDiscoveryAds : IAds
{
    public void Initialize(string appId, Action<IInitialize> iInitialize, List<string> testDevices)
    {
        
    }

    public void LoadGdpr(bool childDirected)
    {
        
    }

    public void LoadInterstitial(string adUnitId)
    {
        
    }

    public void LoadRewards(string adUnitId)
    {
        
    }

    public void HideBanner() {
        
    }

    public void ShowBanner(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad, AdsBannerPosition adsBannerPosition)
    {
        onAdFailedToLoad("sdk not available");
    }

    public void ShowInterstitial(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad)
    {
        onAdFailedToLoad.Invoke("sdk not available");
    }

    public void ShowNativeAds(string adUnitId, Action<CallbackAds> callbackAds)
    {
         var cb = new CallbackAds();
        cb.OnAdFailedToLoad("sdk not available");
        callbackAds(cb);
    }

    public void ShowRewards(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad, Action<IRewards> iRewards)
    {
        onAdFailedToLoad.Invoke("sdk not available");
    }
}