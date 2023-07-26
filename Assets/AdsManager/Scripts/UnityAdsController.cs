using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class UnityAdsController
    : MonoBehaviour,
      IUnityAdsLoadListener,
      IUnityAdsShowListener,
      IUnityAdsInitializationListener
{
    public string unityAdsID;
    public bool isTestMode;
    public BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;
    public UnityAdsInterstitialAd unityAdsInterstitialAd;
    public UnityAdsRewardedAds unityAdsRewardedAds;
    public UnityEvent OnUserEarnedRewardEvent;

    private string bannerAdUnitId = "";

    public void InitializeAds()
    {
        unityAdsID = ConfigApp.configAppModel.unityAdsId;
        isTestMode = ConfigApp.configAppModel.isTestAds;
        Advertisement.Initialize(unityAdsID, isTestMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        if (ConfigApp.configAppModel.adsBanner == "UnityAds" && ConfigApp.configAppModel.isShowBanner)
        {
            LoadBanner();
        }
        if(ConfigApp.configAppModel.adsInter == "UnityAds" && ConfigApp.configAppModel.isShowInt)
        {
            unityAdsInterstitialAd.LoadAd();
        }
        if (ConfigApp.configAppModel.adsRewards == "UnityAds" && ConfigApp.configAppModel.isShowRewards)
        {
            unityAdsRewardedAds.LoadAd();
        }
    }

    public void InitData()
    {
        InitializeAds();
        OnUserEarnedRewardEvent = unityAdsRewardedAds.OnUserEarnedRewardEvent;
        Debug.Log($"InitData UnityAds: gameId: {unityAdsID}");
    }

    public void ShowBanner(AdsBannerPosition adsBannerPosition)
    {
        switch (adsBannerPosition)
        {
            case AdsBannerPosition.TOP_CENTER:
                bannerPosition = BannerPosition.TOP_CENTER;
                break;
            case AdsBannerPosition.BOTTOM_CENTER:
                bannerPosition = BannerPosition.BOTTOM_CENTER;
                break;
        }
        Advertisement.Banner.SetPosition(bannerPosition);
        ShowBannerAd();
    }

    public void DestroyBanner()
    {
        HideBannerAd();
    }

    public void ShowInterstitial()
    {
        // unityAdsInterstitialAd.ShowAd();
    }

    public void ShowRewardedAd()
    {
        // unityAdsRewardedAds.ShowAd();
    }

    public void HandleUserEarnedReward() { }

    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(bannerAdUnitId, options);
    }
           

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log("Banner Error: " + message);
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(unityAdsID, options);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
        Debug.Log("Ad Loaded: " + adUnitId);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(
        string adUnitId,
        UnityAdsShowCompletionState showCompletionState
    ) { }
}
