using System.Collections.Generic;
using Newtonsoft.Json;

namespace EaAds
{
    public class AdConfig
    {
        public bool IsShowAds { get; set; }
        public bool IsShowOpenAd { get; set; }
        public bool IsShowBanner { get; set; }
        public bool IsShowInterstitial { get; set; }
        public bool IsShowNativeAd { get; set; }
        public bool IsShowRewards { get; set; }

        public int IntervalTimeInterstitial { get; set; }

        public string PrimaryOpenAdId { get; set; }
        public string SecondaryOpenAdId { get; set; }
        public string TertiaryOpenAdId { get; set; }
        public string QuaternaryOpenAdId { get; set; }

        [JsonConverter(typeof(NetworkAdsConverter))]
        public NetworkAds PrimaryAds { get; set; }
        [JsonConverter(typeof(NetworkAdsConverter))]
        public NetworkAds SecondaryAds { get; set; }
        [JsonConverter(typeof(NetworkAdsConverter))]
        public NetworkAds TertiaryAds { get; set; }
        [JsonConverter(typeof(NetworkAdsConverter))]
        public NetworkAds QuaternaryAds { get; set; }

        public string PrimaryAppId { get; set; }
        public string SecondaryAppId { get; set; }
        public string TertiaryAppId { get; set; }
        public string QuaternaryAppId { get; set; }

        public string PrimaryBannerId { get; set; }
        public string SecondaryBannerId { get; set; }
        public string TertiaryBannerId { get; set; }
        public string QuaternaryBannerId { get; set; }

        public string PrimaryInterstitialId { get; set; }
        public string SecondaryInterstitialId { get; set; }
        public string TertiaryInterstitialId { get; set; }
        public string QuaternaryInterstitialId { get; set; }

        public string PrimaryNativeId { get; set; }
        public string SecondaryNativeId { get; set; }
        public string TertiaryNativeId { get; set; }
        public string QuaternaryNativeId { get; set; }

        public string PrimaryRewardsId { get; set; }
        public string SecondaryRewardsId { get; set; }
        public string TertiaryRewardsId { get; set; }
        public string QuaternaryRewardsId { get; set; }
        public string SdkKeyAppLovin { get; set; }
        public bool IsAppActive { get; set; }
        public bool IsOnRedirect { get; set; }
        public bool IsOnRedirectAppCancelable { get; set; }
        public string UrlRedirect { get; set; }
        public string UrlPrivacyPolicy { get; set; }

        public List<string> TestDevices { get; set; }
    }
}
