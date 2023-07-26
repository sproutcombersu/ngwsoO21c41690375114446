using System;
using System.Collections.Generic;
using UnityEngine;

public enum AdsBannerPosition
    {
        TOP_LEFT = 0,
        TOP_CENTER = 1,
        TOP_RIGHT = 2,
        BOTTOM_LEFT = 3,
        BOTTOM_CENTER = 4,
        BOTTOM_RIGHT = 5,
        CENTER = 6
    }
public class AdsManager : IAdsManager
{
    private HandleAds handleAds;

    public AdsManager(HandleAds handleAds)
    {
        this.handleAds = handleAds;
    }

    [Obsolete]
    public void Initialize(Action<IInitialize> iInitialize, List<string> testDevices, NetworkAds primaryAds, string primaryAppId, NetworkAds secondaryAds, string secondaryAppId, NetworkAds tertiaryAds, string tertiaryAppId, NetworkAds quaternaryAds, string quaternaryAppId)
    {
        handleAds.Initialize(primaryAppId, iInitialize, testDevices, primaryAds);
        handleAds.Initialize(secondaryAppId, iInitialize, testDevices, secondaryAds);
        handleAds.Initialize(tertiaryAppId, iInitialize, testDevices, tertiaryAds);
        handleAds.Initialize(quaternaryAppId, iInitialize, testDevices, quaternaryAds);
    }

    public void LoadGdpr(bool childDirected, NetworkAds primaryAds, NetworkAds secondaryAds, NetworkAds tertiaryAds, NetworkAds quaternaryAds)
    {

    }

    [Obsolete]
    public void LoadInterstitial(NetworkAds primaryAds, string adUnitPrimaryId, NetworkAds secondaryAds, string adUnitSecondaryId, NetworkAds tertiaryAds, string adUnitTertiaryAdsId, NetworkAds quaternaryAds, string adUnitQuaternaryId)
    {
        handleAds.LoadInterstitial(primaryAds, adUnitPrimaryId);
        handleAds.LoadInterstitial(secondaryAds, adUnitSecondaryId);
        handleAds.LoadInterstitial(tertiaryAds, adUnitTertiaryAdsId);
        handleAds.LoadInterstitial(quaternaryAds, adUnitQuaternaryId);
    }

    public void LoadRewards(NetworkAds primaryAds, string adUnitPrimaryId, NetworkAds secondaryAds, string adUnitSecondaryId, NetworkAds tertiaryAds, string adUnitTertiaryAdsId, NetworkAds quaternaryAds, string adUnitQuaternaryId)
    {
        handleAds.LoadRewards(primaryAds, adUnitPrimaryId);
        handleAds.LoadRewards(secondaryAds, adUnitSecondaryId);
        handleAds.LoadRewards(tertiaryAds, adUnitTertiaryAdsId);
        handleAds.LoadRewards(quaternaryAds, adUnitQuaternaryId);
    }

    public void HideBanner(NetworkAds primaryAds, NetworkAds secondaryAds, NetworkAds tertiaryAds, NetworkAds quaternaryAds)
    {
        handleAds.HideBanner(primaryAds);
        handleAds.HideBanner(secondaryAds);
        handleAds.HideBanner(tertiaryAds);
        handleAds.HideBanner(quaternaryAds);
    }

    [Obsolete]
    public void ShowBanner(NetworkAds primaryAds, string adUnitPrimaryId, NetworkAds secondaryAds, string adUnitSecondaryId, NetworkAds tertiaryAds, string adUnitTertiaryAdsId, NetworkAds quaternaryAds, string adUnitQuaternaryId, Action<string> onAdLoded, Action<string> onAdFailedToLoad, AdsBannerPosition adsBannerPosition)
    {
        HideBanner(primaryAds, secondaryAds, tertiaryAds, quaternaryAds);
       handleAds.ShowBanner(primaryAds, adUnitPrimaryId, onAdLoded, (onAdFailedToLoad1) => {
            if(string.IsNullOrEmpty(adUnitSecondaryId)) onAdFailedToLoad.Invoke(onAdFailedToLoad1);
             handleAds.ShowBanner(secondaryAds, adUnitSecondaryId,onAdLoded, (onAdFailedToLoad2) => {
                 if(string.IsNullOrEmpty(adUnitTertiaryAdsId)) onAdFailedToLoad.Invoke(onAdFailedToLoad2);
                     handleAds.ShowBanner(tertiaryAds, adUnitTertiaryAdsId, onAdLoded, (onAdFailedToLoad3) => {
                            if(string.IsNullOrEmpty(adUnitQuaternaryId)) onAdFailedToLoad.Invoke(onAdFailedToLoad3);
                            handleAds.ShowBanner(quaternaryAds, adUnitQuaternaryId, onAdLoded, onAdFailedToLoad);
                        }, adsBannerPosition);
                }, adsBannerPosition);
        }, adsBannerPosition);
    }

    [Obsolete]
    public void ShowInterstitial(NetworkAds primaryAds, string adUnitPrimaryId, NetworkAds secondaryAds, string adUnitSecondaryId, NetworkAds tertiaryAds, string adUnitTertiaryAdsId, NetworkAds quaternaryAds, string adUnitQuaternaryId, Action<string> onAdLoded, Action<string> onAdFailedToLoad)
    {
        handleAds.ShowInterstitial(primaryAds, adUnitPrimaryId, onAdLoded, (onAdFailedToLoad1) => {
            if(string.IsNullOrEmpty(adUnitSecondaryId)) onAdFailedToLoad.Invoke(onAdFailedToLoad1);
             handleAds.ShowInterstitial(secondaryAds, adUnitSecondaryId,onAdLoded, (onAdFailedToLoad2) => {
                 if(string.IsNullOrEmpty(adUnitTertiaryAdsId)) onAdFailedToLoad.Invoke(onAdFailedToLoad2);
                     handleAds.ShowInterstitial(tertiaryAds, adUnitTertiaryAdsId, onAdLoded, (onAdFailedToLoad3) => {
                            if(string.IsNullOrEmpty(adUnitQuaternaryId)) onAdFailedToLoad.Invoke(onAdFailedToLoad3);
                            handleAds.ShowInterstitial(quaternaryAds, adUnitQuaternaryId, onAdLoded, onAdFailedToLoad);
                        });
                });
        });
    }

    [Obsolete]
    public void ShowNativeAds(NetworkAds primaryAds, string adUnitPrimaryId, NetworkAds secondaryAds, string adUnitSecondaryId, NetworkAds tertiaryAds, string adUnitTertiaryAdsId, NetworkAds quaternaryAds, string adUnitQuaternaryId, Action<CallbackAds> callbackAds)
    {
        /*
        handleAds.ShowInterstitial(primaryAds, adUnitPrimaryId, callbackAds1 => {
            callbackAds(callbackAds1);
            if (!string.IsNullOrEmpty(callbackAds1.ErrorMessage))
            {
                handleAds.ShowInterstitial(secondaryAds, adUnitSecondaryId, callbackAds2 => {
                    callbackAds(callbackAds2);
                    if(!string.IsNullOrEmpty(callbackAds2.ErrorMessage)) {
                        handleAds.ShowInterstitial(tertiaryAds, adUnitTertiaryAdsId, callbackAds3 => {
                            callbackAds(callbackAds3);
                            if(!string.IsNullOrEmpty(callbackAds3.ErrorMessage)) {
                                handleAds.ShowInterstitial(quaternaryAds, adUnitQuaternaryId, callbackAds);
                            }
                        });
                    }
                });
            }
        });
        */
    }

    public void ShowRewards(NetworkAds primaryAds, string adUnitPrimaryId, NetworkAds secondaryAds, string adUnitSecondaryId, NetworkAds tertiaryAds, string adUnitTertiaryAdsId, NetworkAds quaternaryAds, string adUnitQuaternaryId, Action<string> onAdLoded, Action<string> onAdFailedToLoad, Action<IRewards> iRewards)
    {
       handleAds.ShowRewards(primaryAds, adUnitPrimaryId, onAdLoded, (onAdFailedToLoad1) => {
            if(string.IsNullOrEmpty(adUnitSecondaryId)) onAdFailedToLoad.Invoke(onAdFailedToLoad1);
             handleAds.ShowRewards(secondaryAds, adUnitSecondaryId,onAdLoded, (onAdFailedToLoad2) => {
                if(string.IsNullOrEmpty(adUnitTertiaryAdsId)) onAdFailedToLoad.Invoke(onAdFailedToLoad2);
                handleAds.ShowRewards(tertiaryAds, adUnitTertiaryAdsId, onAdLoded, (onAdFailedToLoad3) => {
                        if(string.IsNullOrEmpty(adUnitQuaternaryId)) onAdFailedToLoad.Invoke(onAdFailedToLoad3);
                        handleAds.ShowRewards(quaternaryAds, adUnitQuaternaryId, onAdLoded, onAdFailedToLoad, iRewards);
                    }, iRewards);
                }, iRewards);
        }, iRewards);
    }
}
