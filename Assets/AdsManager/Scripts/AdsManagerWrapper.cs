using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManagerWrapper : MonoBehaviour
{
    public bool isShowAds = false;
    public bool isShowOpenAd = false;
    public bool isShowBanner = false;
    public bool isShowInterstitial = false;
    public bool isShowNativeAd = false;
    public bool isShowRewards = false;

    public int intervalTimeInterstitial = 10; // seconds

    [Header("Open Ad Id")]
    public string primaryOpenAdId = "";
    public string secondaryOpenAdId = "";
    public string tertiaryOpenAdId = "";
    public string quaternaryOpenAdId = "";

    [Header("Network Ads")]
    public NetworkAds primaryAds = NetworkAds.NONE;
    public NetworkAds secondaryAds = NetworkAds.NONE;
    public NetworkAds tertiaryAds = NetworkAds.NONE;
    public NetworkAds quaternaryAds = NetworkAds.NONE;

    [Header("App Id")]
    public string primaryAppId = "";
    public string secondaryAppId = "";
    public string tertiaryAppId = "";
    public string quaternaryAppId = "";

    [Header("Banner Id")]
    public string primaryBannerId = "";
    public string secondaryBannerId = "";
    public string tertiaryBannerId = "";
    public string quaternaryBannerId = "";

    [Header("Interstitial Id")]
    public string primaryInterstitialId = "";
    public string secondaryInterstitialId = "";
    public string tertiaryInterstitialId = "";
    public string quaternaryInterstitialId = "";

    [Header("Native Id")]
    public string primaryNativeId = "";
    public string secondaryNativeId = "";
    public string tertiaryNativeId = "";
    public string quaternaryNativeId = "";

    [Header("Rewards Id")]
    public string primaryRewardsId = "";
    public string secondaryRewardsId = "";
    public string tertiaryRewardsId = "";
    public string quaternaryRewardsId = "";

    public List<String> testDevices;

    public static AdsManagerWrapper INSTANCE;

    private AdsManager adsManager;

    private AdmobAds admobAds;
    private FanAds fanAds;
    private UnityAdsNetwork unityAdsNetwork;
    private ApplovinMaxAds applovinMaxAds;

    private void Awake() {
        if(INSTANCE != null) {
            Destroy(this);
        }
        // if(!admobAds) {
        //     admobAds = gameObject.AddComponent<AdmobAds>();
        // }
        if(!fanAds) {
            fanAds = gameObject.AddComponent<FanAds>();
        }
        if(!unityAdsNetwork) {
            unityAdsNetwork = gameObject.AddComponent<UnityAdsNetwork>();
        }
        if(!applovinMaxAds) {
            applovinMaxAds = gameObject.AddComponent<ApplovinMaxAds>();
        }
    }

    // Start is called before the first frame update
    void Start() { 
        INSTANCE = this;
        DontDestroyOnLoad(this);
        //  if(!unityAdsNetwork) {
        //     unityAdsNetwork = gameObject.GetComponent<UnityAdsNetwork>();
        // }
        var handleAds = new HandleAds(new AdmobAds(), fanAds, applovinMaxAds, new ApplovinDiscoveryAds(),  unityAdsNetwork);
        adsManager = new AdsManager(handleAds);
    }

    [Obsolete]
    public void Initialize(Action<IInitialize> iInitialize)
    {
       if(isShowAds) {
            adsManager.Initialize(iInitialize, testDevices, primaryAds, primaryAppId, secondaryAds, secondaryAppId, tertiaryAds, tertiaryAppId, quaternaryAds, quaternaryAppId);
       }
    }

    public void LoadGdpr(bool childDirected)
    {
    //    if(isShowAds) {
    //         adsManager.LoadGdpr(childDirected, primaryAds, primaryAppId, secondaryAds, secondaryAppId, tertiaryAds, tertiaryAppId, quaternaryAds, quaternaryAppId);
    //    }
    }


    [Obsolete]
    public void HideBanner()
    {
       if(isShowAds && isShowBanner) {
            adsManager.HideBanner(primaryAds, secondaryAds, tertiaryAds, quaternaryAds);
       }
    }

    [Obsolete]
    public void ShowBanner(Action<string> onAdLoded, Action<string> onAdFailedToLoad, AdsBannerPosition adsBannerPosition = AdsBannerPosition.BOTTOM_CENTER)
    {
       if(isShowAds && isShowBanner) {
            adsManager.ShowBanner(primaryAds, primaryBannerId, secondaryAds, secondaryBannerId, tertiaryAds, tertiaryBannerId, quaternaryAds, quaternaryBannerId, onAdLoded, onAdFailedToLoad, adsBannerPosition);
       }
    }

    [Obsolete]
    public void LoadInterstitial()
    {
       if(isShowAds && isShowInterstitial) {
            adsManager.LoadInterstitial(primaryAds, primaryInterstitialId, secondaryAds, secondaryInterstitialId, tertiaryAds, tertiaryInterstitialId, quaternaryAds, quaternaryInterstitialId);
       }
    }

    [Obsolete]
    public void ShowInterstitial(Action<string> onAdLoded, Action<string> onAdFailedToLoad)
    {
       if(isShowAds && isShowInterstitial && IsValidBetweenTimeInterstitial()) {
            adsManager.ShowInterstitial(primaryAds, primaryInterstitialId, secondaryAds, secondaryInterstitialId, tertiaryAds, tertiaryInterstitialId, quaternaryAds, quaternaryInterstitialId, onAdLoded, onAdFailedToLoad);
       }
    }

    public void ShowNativeAds(Action<CallbackAds> callbackAds)
    {
       
    }

    public void LoadRewards()
    {
        if(isShowAds && isShowRewards) {
            adsManager.LoadRewards(primaryAds, primaryRewardsId, secondaryAds, secondaryRewardsId, tertiaryAds, tertiaryRewardsId, quaternaryAds, quaternaryRewardsId);
       }
    }

    public void ShowRewards(Action<string> onAdLoded, Action<string> onAdFailedToLoad, Action<IRewards> iRewards)
    {
        if(isShowAds && isShowRewards) {
            adsManager.ShowRewards(primaryAds, primaryRewardsId, secondaryAds, secondaryRewardsId, tertiaryAds, tertiaryRewardsId, quaternaryAds, quaternaryRewardsId, onAdLoded, onAdFailedToLoad, iRewards);
       }
    }

    private DateTime lastDate = DateTime.Now;

    public bool IsValidBetweenTimeInterstitial()
    {
        var currentDate = DateTime.Now;
        var diffSeconds = (currentDate - lastDate).TotalSeconds;

        if (diffSeconds >= intervalTimeInterstitial)
        {
            lastDate = currentDate;
            return true;
        }

        return false;
    }

}
