using System;
using System.Collections.Generic;
using AudienceNetwork;
using UnityEngine;

public class FanAds : MonoBehaviour, IAds
{
    private AdView adView;
    private InterstitialAd interstitialAd;
    private bool isLoaded;

    public void Initialize(string appId, Action<IInitialize> iInitialize, List<string> testDevices)
    {
        AudienceNetworkAds.Initialize();
        foreach (var item in testDevices)
        {
            AdSettings.AddTestDevice(item);
        }
    }

    public void LoadGdpr(bool childDirected)
    {
        throw new NotImplementedException();
    }

    public void LoadInterstitial(string adUnitId)
    {
        this.interstitialAd = new InterstitialAd(adUnitId);
        this.interstitialAd.Register(this.gameObject);

        // Set delegates to get notified on changes or when the user interacts with the ad.
        this.interstitialAd.InterstitialAdDidLoad = (
            delegate()
            {
                Debug.Log("Interstitial ad loaded.");
                this.isLoaded = true;
            }
        );
        interstitialAd.InterstitialAdDidFailWithError = (
            delegate(string error)
            {
                Debug.Log("Interstitial ad failed to load with error: " + error);
                this.isLoaded = false;
            }
        );
        interstitialAd.InterstitialAdWillLogImpression = (
            delegate()
            {
                Debug.Log("Interstitial ad logged impression.");
            }
        );
        interstitialAd.InterstitialAdDidClick = (
            delegate()
            {
                Debug.Log("Interstitial ad clicked.");
            }
        );

        this.interstitialAd.interstitialAdDidClose = (
            delegate()
            {
                Debug.Log("Interstitial ad did close.");
                if (this.interstitialAd != null)
                {
                    this.interstitialAd.Dispose();
                }
                LoadInterstitial(adUnitId);
            }
        );

        // Initiate the request to load the ad.
        this.interstitialAd.LoadAd();
    }

    private bool isRewardsLoaded;
    private RewardedVideoAd rewardedVideoAd;
    Action<IRewards> iRewards;
    public void LoadRewards(string adUnitId)
    {
        //Set the rewarded ad data
        RewardData rewardData = new RewardData();

        // Instantiate RewardedVideoAd with reward data
        this.rewardedVideoAd = new RewardedVideoAd(adUnitId, rewardData);
        this.rewardedVideoAd.Register(this.gameObject);

        // Set delegates to get notified on changes or when the user interacts with the ad.
        this.rewardedVideoAd.RewardedVideoAdDidLoad = (delegate() {
            Debug.Log("RewardedVideo ad loaded.");
            this.isRewardsLoaded = true;
        });
        this.rewardedVideoAd.RewardedVideoAdDidFailWithError = (delegate(string error) {
            Debug.Log("RewardedVideo ad failed to load with error: " + error);
            this.isRewardsLoaded = false;
        });
        this.rewardedVideoAd.RewardedVideoAdWillLogImpression = (delegate() {
            Debug.Log("RewardedVideo ad logged impression.");
        });
        this.rewardedVideoAd.RewardedVideoAdDidClick = (delegate() {
            Debug.Log("RewardedVideo ad clicked.");
        });
        this.rewardedVideoAd.RewardedVideoAdDidClose = (delegate() {
            Debug.Log("Rewarded video ad did close.");
            if (this.rewardedVideoAd != null) {
                this.rewardedVideoAd.Dispose();
            }
            LoadRewards(adUnitId);
        });

        var _iRewards = new IRewards();
        // For S2S validation you need to register the following two callback
        this.rewardedVideoAd.RewardedVideoAdDidSucceed = (delegate() {
            Debug.Log("Rewarded video ad validated by server");
             var rewardsItem = new MyRewardsItem();
                    rewardsItem.Amount = 10;
                    rewardsItem.Type = "rewards";
            _iRewards.OnUserEarnedReward(rewardsItem);
            iRewards(_iRewards);
            
        });
        this.rewardedVideoAd.RewardedVideoAdDidFail = (delegate() {
            Debug.Log("Rewarded video ad not validated, or no response from server");
        });       

        this.rewardedVideoAd.LoadAd();
    }


    public void HideBanner() {
        if(this.adView!=null) {
            this.adView.Dispose();
        }
    }

    public void ShowBanner(
        string adUnitId,
         Action<string> onAdLoded, Action<string> onAdFailedToLoad,
        AdsBannerPosition adsBannerPosition
    )
    {
        try
        {
            // Kode yang mungkin memunculkan exception
            if (this.adView)
            {
                this.adView.Dispose();
            }

            this.adView = new AdView(adUnitId, AdSize.BANNER_HEIGHT_50);
            this.adView.Register(this.gameObject);

            // Set delegates to get notified on changes or when the user interacts with the ad.
            this.adView.AdViewDidLoad = (
                delegate()
                {
                    Debug.Log("Banner loaded.");
                    var posisition = AdPosition.BOTTOM;
                    if (adsBannerPosition == AdsBannerPosition.TOP_CENTER)
                    {
                        posisition = AdPosition.TOP;
                    }
                    this.adView.Show(posisition);
                    onAdLoded("Fan Banner loaded.");
                }
            );
            adView.AdViewDidFailWithError = (
                delegate(string error)
                {
                    Debug.Log("Banner failed to load with error: " + error);
                    onAdFailedToLoad("Banner failed to load with error: " + error);
                }
            );
            adView.AdViewWillLogImpression = (
                delegate()
                {
                    Debug.Log("Banner logged impression.");
                }
            );
            adView.AdViewDidClick = (
                delegate()
                {
                    Debug.Log("Banner clicked.");
                }
            );

            // Initiate a request to load an ad.
            adView.LoadAd();
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

    public void ShowInterstitial(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad)
    {
        // Kode yang mungkin memunculkan exception
        if (this.isLoaded)
        {
            this.interstitialAd.Show();
            onAdLoded.Invoke("fan int show");
        }
        else
        {
            LoadInterstitial(adUnitId);
            onAdFailedToLoad.Invoke("fan int failed to show");
        }
        this.isLoaded = false;
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
        this.iRewards = iRewards;
        var cb = new CallbackAds();
        if (this.isRewardsLoaded) {
            this.rewardedVideoAd.Show();
            onAdLoded("");
        } else {
            onAdFailedToLoad("Ad not loaded. Click load to request an ad.");
            Debug.Log("Ad not loaded. Click load to request an ad.");
            LoadRewards(adUnitId);
        }
        this.isRewardsLoaded = false;
    }
}
