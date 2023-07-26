using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsInterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] 
    public string _androidAdUnitId = "Interstitial_Android";
    [SerializeField]
    public string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;

    private bool isReady = false;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        // callbackIntersitial = new CallbackAds();
        Advertisement.Load(_adUnitId, this);
    }

    // Show the loaded content in the Ad Unit: 
    public void ShowAd(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad)
    {
        this._adUnitId = adUnitId;
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        if(isReady) {
            Advertisement.Show(_adUnitId, this);
            onAdLoded.Invoke("unityAds int show");
        }else {
            onAdFailedToLoad.Invoke("unityAds int failed to show");
            LoadAd();
        }
        isReady = false;
    }

    // Implement Load Listener and Show Listener interface methods:  
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        isReady = true;
        // Optionally execute code if the Ad Unit successfully loads content.
        Debug.Log("OnUnityAdsAdLoaded: " + adUnitId);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        isReady = false;
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        isReady = false;
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }
}