using System;
using System.Collections.Generic;
public interface IAds
{
   void Initialize(string appId, Action <IInitialize> iInitialize, List<string> testDevices);
   void LoadGdpr(bool childDirected);
   void ShowBanner(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad, AdsBannerPosition adsBannerPosition = AdsBannerPosition.BOTTOM_CENTER);
   void HideBanner();
   void LoadInterstitial(string adUnitId);
   void ShowInterstitial(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad);
   void ShowNativeAds(string adUnitId, Action<CallbackAds> callbackAds);
   void LoadRewards(string adUnitId);
   void ShowRewards(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad, Action <IRewards> iRewards);
}
