using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsNetwork : MonoBehaviour, IAds, IUnityAdsInitializationListener
{
    private UnityAdsInterstitialAd unityAdsInterstitialAd;
    private Action<CallbackAds> callbackIntersitial;
    private UnityAdsRewardedAds unityAdsRewardedAds;
    private Action<CallbackAds> callbackRewardsAds;

    void Start()
    {
        if (!unityAdsInterstitialAd)
        {
            unityAdsInterstitialAd = gameObject.AddComponent<UnityAdsInterstitialAd>();
        }
        
        if (!unityAdsRewardedAds)
        {
            unityAdsRewardedAds = gameObject.AddComponent<UnityAdsRewardedAds>();
        }
        
    }

    public void Initialize(string appId, Action<IInitialize> iInitialize, List<string> testDevices)
    {
        try
        {
            //Kode yang mungkin memunculkan exception
            if(!string.IsNullOrEmpty(appId))
            Advertisement.Initialize(appId, false, this);
            // Advertisement.Banner.Show(adUnitId);
        }
         catch (System.DivideByZeroException e)
        {
            // Exception yang dihasilkan oleh pembagian dengan nol
            Debug.LogError("Terjadi kesalahan: " + e.Message);
        }
        catch (System.Exception e)
        {
            // Exception umum lainnya
            Debug.LogError("Terjadi kesalahan: " + e.Message);
        }
        finally
        {
           
        }
    }

    public void LoadGdpr(bool childDirected)
    {
        throw new NotImplementedException();
    }

    public void LoadInterstitial(string adUnitId)
    {
        unityAdsInterstitialAd._androidAdUnitId = adUnitId;
        unityAdsInterstitialAd.LoadAd();
    }

    public void LoadRewards(string adUnitId)
    {
        unityAdsRewardedAds._androidAdUnitId = adUnitId;
        unityAdsRewardedAds.LoadAd();
    }

    public void HideBanner() {
        Advertisement.Banner.Hide();
    }

    private Action<CallbackAds> callbackBanner;
    
    private bool bannerIsReady = false;
    private string bannerID = "Banner_Android";

    private void LoadBanner() {
          //Kode yang mungkin memunculkan exception
            BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;
            // switch (adsBannerPosition)
            // {
            //     case AdsBannerPosition.TOP_CENTER:
            //         bannerPosition = BannerPosition.TOP_CENTER;
            //         break;
            //     case AdsBannerPosition.BOTTOM_CENTER:
            //         bannerPosition = BannerPosition.BOTTOM_CENTER;
            //         break;
            // }
            Advertisement.Banner.SetPosition(bannerPosition);

            // Set up options to notify the SDK of load events:
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = () => {
                    bannerIsReady = true;
                },
                errorCallback = (error) => {
                    bannerIsReady = false;
                }
            };

            // Load the Ad Unit with banner content:
            Advertisement.Banner.Load(bannerID, options);
    }

    public void ShowBanner(
        string adUnitId,
        Action<string> onAdLoded, Action<string> onAdFailedToLoad,
        AdsBannerPosition adsBannerPosition
    )
    {
        try
        {
            bannerID = adUnitId;
            if(bannerIsReady) {
                Advertisement.Banner.Show(adUnitId);
                onAdLoded("unity ads load");
            } else {
                LoadBanner();
                Advertisement.Banner.Hide();
                onAdFailedToLoad("unity Ads not show");
            }
            bannerIsReady = false;

        }
        catch (System.DivideByZeroException e)
        {
            // Exception yang dihasilkan oleh pembagian dengan nol
            Debug.LogError("Terjadi kesalahan: " + e.Message);
            // onAdFailedToLoad(e.Message);
        }
        catch (System.Exception e)
        {
            // Exception umum lainnya
            Debug.LogError("Terjadi kesalahan: " + e.Message);
            // onAdFailedToLoad(e.Message);
        }
        finally
        {
            // Blok finally akan selalu dijalankan
            Debug.Log("Akhir dari blok try-catch");
        }
    }

    public void ShowInterstitial(string adUnitId, Action<string> onAdLoded, Action<string> onAdFailedToLoad)
    {
        unityAdsInterstitialAd.ShowAd(adUnitId, onAdLoded, onAdFailedToLoad);
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
        unityAdsRewardedAds.ShowAd(adUnitId, onAdLoded, onAdFailedToLoad, iRewards);
    }

    void IUnityAdsInitializationListener.OnInitializationComplete() {
        LoadBanner();
     }

    void IUnityAdsInitializationListener.OnInitializationFailed(
        UnityAdsInitializationError error,
        string message
    ) { 

    }
}