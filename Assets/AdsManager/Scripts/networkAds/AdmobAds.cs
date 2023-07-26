using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

public class AdmobAds : IAds
{
    private InterstitialAd interstitialAd;
    private BannerView bannerView;

    #region HELPER METHODS

    [Obsolete]
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #endregion


    [Obsolete]
    public void Initialize(string appId, Action<IInitialize> iInitialize, List<string> testDevices)
    {
        MobileAds.SetiOSAppPauseOnBackground(true);
        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };
        deviceIds.AddRange(testDevices);

        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
            .SetTestDeviceIds(deviceIds)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        var _iInitialize = new IInitialize();

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(
            (e) =>
            {
                _iInitialize.OnInitializationComplete();
                iInitialize(_iInitialize);
            }
        );
    }

    public void LoadGdpr(bool childDirected) { }

    [Obsolete]
    public void LoadInterstitial(string adUnitId)
    {   
        Debug.Log("LoadInterstitial admob: " + adUnitId);
        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
        // interstitialAd = new InterstitialAd(adUnitId);
        var adRequest = new AdRequest();

        // Load an interstitial ad
        InterstitialAd.Load(
            adUnitId,
            adRequest,
            (InterstitialAd ad, LoadAdError loadAdError) =>
            {
                 // If the operation failed with a reason.
                if (loadAdError != null)
                {
                    Debug.LogError("Interstitial ad failed to load an ad with error : " + loadAdError);
                    return;
                }
                // If the operation failed for unknown reasons.
                // This is an unexpected error, please report this bug if it happens.
                if (ad == null)
                {
                    Debug.LogError("Unexpected error: Interstitial load event fired with null ad and null error.");
                    return;
                }
                interstitialAd = ad;
                RegisterReloadHandler(adUnitId, ad);
            }
        );

    }

    private RewardedAd rewardedAd;

    public void LoadRewards(string adUnitId)
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(
            adUnitId,
            adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad l oaded with response : " + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterReloadHandlerRewards(adUnitId, rewardedAd);
            }
        );
    }

    public void HideBanner() {
        if(bannerView != null) {
            bannerView.Hide();
        }
    }

    [Obsolete]
    public void ShowBanner(
        string adUnitId,
        Action<string> onAdLoded, Action<string> onAdFailedToLoad,
        AdsBannerPosition adsBannerPosition
    )
    {
        try
        {
            // Kode yang mungkin memunculkan exception
            if (bannerView != null)
            {
                bannerView.Destroy();
            }

            var bannerPosition = AdPosition.Bottom;
            switch (adsBannerPosition)
            {
                case AdsBannerPosition.TOP_CENTER:
                    bannerPosition = AdPosition.Top;
                    break;
                case AdsBannerPosition.BOTTOM_CENTER:
                    bannerPosition = AdPosition.Bottom;
                    break;
            }
            // Create a 320x50 banner at top of the screen
            bannerView = new BannerView(adUnitId, AdSize.Banner, bannerPosition);

            // Add Event Handlers
            bannerView.OnBannerAdLoaded += () =>
            {
              onAdLoded("");
            };
            bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                onAdFailedToLoad(error.GetMessage() + "adUnit: " + adUnitId);
            };

            // Load a banner ad
            bannerView.LoadAd(CreateAdRequest());
        }
        catch (System.DivideByZeroException e)
        {
            // Exception yang dihasilkan oleh pembagian dengan nol
            Debug.LogError("Terjadi kesalahan: " + e.Message);
            onAdFailedToLoad(e.Message);
        }
        catch (System.Exception e)
        {
            // Exception umum lainnya
            Debug.LogError("Terjadi kesalahan: " + e.Message);
            onAdFailedToLoad(e.Message);
        }
        finally
        {
            // Blok finally akan selalu dijalankan
            Debug.Log("Akhir dari blok try-catch");
            
        }
    }

    [Obsolete]
    public void ShowInterstitial(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad)
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
            onAdLoded.Invoke("admob int show");
        }
        else
        {
            LoadInterstitial(adUnitId);
            onAdFailedToLoad.Invoke("admob int failed to show");
        }
    }

    public void ShowNativeAds(string adUnitId, Action<CallbackAds> callbackAds)
    {
        throw new NotImplementedException();
    }

    public void ShowRewards(
        string adUnitId,
        Action<string> onAdLoded, Action<string> onAdFailedToLoad,
        Action<IRewards> iRewards
    )
    {
        var cb = new CallbackAds();
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            var _iRewards = new IRewards();
            onAdLoded("CanShowAd");
            rewardedAd.Show(
                (Reward reward) => {
                    // TODO: Reward the user.
                    // Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                    var rewardsItem = new MyRewardsItem();
                    rewardsItem.Amount = 10;
                    rewardsItem.Type = "rewards";
                    _iRewards.OnUserEarnedReward(rewardsItem);
                    iRewards(_iRewards);

                }
            );
        } else {
            onAdFailedToLoad("Can't Show Ads");
            LoadRewards(adUnitId);
        }
    }

    [Obsolete]
    private void RegisterReloadHandler(string adUnitId, InterstitialAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitial(adUnitId);
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError(
                "Interstitial ad failed to open full screen content " + "with error : " + error
            );

            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitial(adUnitId);
        };
    }

    private void RegisterReloadHandlerRewards(string adUnitId,RewardedAd ad)
{
    // Raised when the ad closed full screen content.
    ad.OnAdFullScreenContentClosed += () =>
    {
        Debug.Log("Rewarded Ad full screen content closed.");

        // Reload the ad so that we can show another as soon as possible.
        LoadRewards(adUnitId);
    };
    // Raised when the ad failed to open full screen content.
    ad.OnAdFullScreenContentFailed += (AdError error) =>
    {
        Debug.LogError("Rewarded ad failed to open full screen content " +
                       "with error : " + error);

        // Reload the ad so that we can show another as soon as possible.
        LoadRewards(adUnitId);
    };
}

}
