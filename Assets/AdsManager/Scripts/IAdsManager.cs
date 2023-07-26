using System;
using System.Collections.Generic;

public interface IAdsManager {
    void Initialize(
        Action <IInitialize> iInitialize, 
        List<string> testDevices,
        NetworkAds primaryAds,
        string primaryAppId,
        NetworkAds secondaryAds,
        string secondaryAppId,
        NetworkAds tertiaryAds,
        string tertiaryAppId,
        NetworkAds quaternaryAds,
        string quaternaryAppId
    );

    void LoadGdpr(
        bool childDirected,
        NetworkAds primaryAds,
        NetworkAds secondaryAds,
        NetworkAds tertiaryAds,
        NetworkAds quaternaryAds
    );

    void ShowBanner(
        NetworkAds primaryAds,
        string adUnitPrimaryId,
        NetworkAds secondaryAds,
        string adUnitSecondaryId,
        NetworkAds tertiaryAds,
        string adUnitTertiaryAdsId,
        NetworkAds quaternaryAds,
        string adUnitQuaternaryId,
        Action<string> onAdLoded, Action<string> onAdFailedToLoad,
        AdsBannerPosition adsBannerPosition = AdsBannerPosition.BOTTOM_CENTER
    );

    void LoadInterstitial(
        NetworkAds primaryAds,
        string adUnitPrimaryId,
        NetworkAds secondaryAds,
        string adUnitSecondaryId,
        NetworkAds tertiaryAds,
        string adUnitTertiaryAdsId,
        NetworkAds quaternaryAds,
        string adUnitQuaternaryId
    );

    void ShowInterstitial(
        NetworkAds primaryAds,
        string adUnitPrimaryId,
        NetworkAds secondaryAds,
        string adUnitSecondaryId,
        NetworkAds tertiaryAds,
        string adUnitTertiaryAdsId,
        NetworkAds quaternaryAds,
        string adUnitQuaternaryId,
        Action<string> onAdLoded, Action<string> onAdFailedToLoad
    );

    void ShowNativeAds(
        NetworkAds primaryAds,
        string adUnitPrimaryId,
        NetworkAds secondaryAds,
        string adUnitSecondaryId,
        NetworkAds tertiaryAds,
        string adUnitTertiaryAdsId,
        NetworkAds quaternaryAds,
        string adUnitQuaternaryId,
        Action<CallbackAds> callbackAds
    );

    void LoadRewards(
        NetworkAds primaryAds,
        string adUnitPrimaryId,
        NetworkAds secondaryAds,
        string adUnitSecondaryId,
        NetworkAds tertiaryAds,
        string adUnitTertiaryAdsId,
        NetworkAds quaternaryAds,
        string adUnitQuaternaryId
    );

    void ShowRewards(
        NetworkAds primaryAds,
        string adUnitPrimaryId,
        NetworkAds secondaryAds,
        string adUnitSecondaryId,
        NetworkAds tertiaryAds,
        string adUnitTertiaryAdsId,
        NetworkAds quaternaryAds,
        string adUnitQuaternaryId,
        Action<string> onAdLoded, Action<string> onAdFailedToLoad,
        Action<IRewards> iRewards
    );
}
