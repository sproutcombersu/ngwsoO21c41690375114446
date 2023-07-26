using System;
using System.Collections.Generic;

public class ConfigAppModel
{
    public string typeAds { get; set; }
    public bool isShowIntAfterSplash { get; set; }
    public bool isAutoRedirect { get; set; }
    public string _id { get; set; }
    public string title { get; set; }
    public string packageName { get; set; }
    public string user { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int ConfigAppID { get; set; }
    public string admobAppId { get; set; }
    public string admobBannerId { get; set; }
    public string admobInterId { get; set; }
    public string admobNativeId { get; set; }
    public string admobOpenAppId { get; set; }
    public string admobRewardId { get; set; }
    public int intervalInt { get; set; }
    public bool isShowAds { get; set; }
    public bool isShowInt { get; set; }
    public bool isShowBanner { get; set; }
    public bool isShowRewards { get; set; }
    public bool isTestAds { get; set; }
    public bool isReviewMode { get; set; }
    public string unityAdsId { get; set; }
    public string urlRedirect { get; set; }
    public string adsBanner { get; set; }
    public string adsInter { get; set; }
    public string adsRewards { get; set; }
    public List<String> testDevices { get; set; }
}

public static class ConfigApp
{
    public static ConfigAppModel configAppModel;
}
