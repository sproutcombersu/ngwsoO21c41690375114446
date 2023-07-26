using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class UnityAdsRewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    public UnityEvent OnUserEarnedRewardEvent;
    string _adUnitId;
    bool isReady = false;
    public Action<CallbackAds> callbackRewards;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        isReady = true;
    }

    private Action<IRewards> iRewards;
    // Implement a method to execute when the user clicks the button.
    public void ShowAd(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad, Action<IRewards> iRewards)
    {
        this._adUnitId = adUnitId;
        this.iRewards = iRewards;
        // Debug.Log("ShowAd: " + _adUnitId);
        // Then show the ad:
        if(isReady) {
            Advertisement.Show(adUnitId, this);
            onAdLoded("unity is ready");
        }
        else {
            onAdFailedToLoad("unity not ready");
            LoadAd();
        }
        isReady = false;
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            // OnUserEarnedRewardEvent.Invoke();
            Debug.Log("Unity Ads Rewarded Ad Completed");
            var _iRewards = new IRewards();
            var myRewardsItem = new MyRewardsItem();
            myRewardsItem.Amount = 10;
            myRewardsItem.Type = "Rewards";
            _iRewards.OnUserEarnedReward(myRewardsItem);
            this.iRewards(_iRewards);
            LoadAd();
            // Grant a reward.

            // Load another ad:
            // Advertisement.Load(_adUnitId, this);
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
        isReady = false;
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        isReady = false;
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
    }
}