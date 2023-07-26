// using System;
// using UnityEngine;

// public enum AdsBannerPosition
//     {
//         TOP_LEFT = 0,
//         TOP_CENTER = 1,
//         TOP_RIGHT = 2,
//         BOTTOM_LEFT = 3,
//         BOTTOM_CENTER = 4,
//         BOTTOM_RIGHT = 5,
//         CENTER = 6
//     }
// public class AdsManagerOld : MonoBehaviour
// {
//     public static AdsManagerOld instance;

//     public GoogleAdMobController googleAdMobController;
//     public UnityAdsController unityAdsController;
//     private int amountRewards = 0;
//     private AudioSource audioSource;
//     public AudioClip soundRewards;

//     private void Awake()
//     {
//         if (instance != null)
//             Destroy(this);
//         googleAdMobController = this.gameObject.GetComponent<GoogleAdMobController>();
//         unityAdsController = this.gameObject.GetComponent<UnityAdsController>();
//         if (!audioSource)
//             audioSource = this.gameObject.AddComponent<AudioSource>();
//     }

//     // Use this for initialization
//     void Start()
//     {
//         instance = this;
//         DontDestroyOnLoad(this);

//         if (ConfigApp.configAppModel.isShowAds)
//         {
//             googleAdMobController.InitData(ConfigApp.configAppModel.testDevices);
//             googleAdMobController.OnUserEarnedRewardEvent.AddListener(HandleUserEarnedReward);
//             unityAdsController.InitData();
//             unityAdsController.OnUserEarnedRewardEvent.AddListener(HandleUserEarnedReward);
//         }
//     }

//     public void ShowBanner(AdsBannerPosition adsBannerPosition=AdsBannerPosition.TOP_CENTER)
//     {
//         if (ConfigApp.configAppModel.isShowAds && ConfigApp.configAppModel.isShowBanner)
//         {
//             if (ConfigApp.configAppModel.adsBanner == "Admob")
//             {
//                 googleAdMobController.RequestBannerAd(adsBannerPosition, "", (error) => {
//                 });
//             }
//             else if (ConfigApp.configAppModel.adsBanner == "UnityAds")
//             {
//                 unityAdsController.ShowBanner(adsBannerPosition);
//             }
//         }
//     }

//     public void ShowBannerBtn(int position) {
//         switch (position)
//         {
//             case 1:
//                 ShowBanner(AdsBannerPosition.TOP_CENTER);
//                 break;
//             case 2:
//                 ShowBanner(AdsBannerPosition.BOTTOM_CENTER);
//                 break;
//         }

//     }
//     public void DestroyBanner()
//     {
//         if (ConfigApp.configAppModel.isShowAds)
//         {
//             if (ConfigApp.configAppModel.adsBanner == "Admob")
//             {
//                 googleAdMobController.DestroyBannerAd();
//             }
//             else if (ConfigApp.configAppModel.adsBanner == "UnityAds")
//             {
//                 unityAdsController.DestroyBanner();
//             }
//         }
//     }

//     int counterInt = 0;
//     public void ShowInterstitial()
//     {
//         counterInt++;
//         if (ConfigApp.configAppModel.isShowAds && ConfigApp.configAppModel.isShowInt && counterInt % ConfigApp.configAppModel.intervalInt == 0)
//         {
//             if (ConfigApp.configAppModel.adsInter == "Admob")
//             {
//                 googleAdMobController.ShowInterstitialAd();
//             }
//             else if (ConfigApp.configAppModel.adsInter == "UnityAds")
//             {
//                 unityAdsController.ShowInterstitial();
//             }
//         }
//     }

//     public void ShowRewards(int amount)
//     {
//         if (ConfigApp.configAppModel.isShowAds && ConfigApp.configAppModel.isShowRewards)
//         {
//             amountRewards = amount;
//             if (ConfigApp.configAppModel.adsRewards == "Admob")
//             {
//                 googleAdMobController.ShowRewardedAd();
//             }
//             else if (ConfigApp.configAppModel.adsRewards == "UnityAds")
//             {
//                 unityAdsController.ShowRewardedAd();
//             }
//         }
//     }

//     Action<bool> callbackRewards;
//      public void ShowRewards(Action<bool> callback)
//     {
//         callbackRewards = callback;
//         if (ConfigApp.configAppModel.isShowAds && ConfigApp.configAppModel.isShowRewards)
//         {
//             if (ConfigApp.configAppModel.adsRewards == "Admob")
//             {
//                 googleAdMobController.ShowRewardedAd();
//             }
//             else if (ConfigApp.configAppModel.adsRewards == "UnityAds")
//             {
//                 unityAdsController.ShowRewardedAd();
//             }
//         }
//     }

//     public void HandleUserEarnedReward()
//     {
//         callbackRewards(true);
//         MonoBehaviour.print("HandleUserEarnedReward: " + amountRewards);
//         if (soundRewards)
//             audioSource.PlayOneShot(soundRewards, 1f);
//     }
// }
