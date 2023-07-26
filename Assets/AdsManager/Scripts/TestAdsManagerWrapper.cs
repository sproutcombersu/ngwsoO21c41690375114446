using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAdsManagerWrapper : MonoBehaviour
{
    public Text txtLog;
    public Button btnInitData;
    public Button btnShowBanner;
    public Button btnHideBanner;
    public Button btnShowInter;
    public Button btnShowRewards;

    [System.Obsolete]
    private void Start() {
        btnInitData.onClick.AddListener(InitData);
        btnShowBanner.onClick.AddListener(ShowBanner);
        btnHideBanner.onClick.AddListener(HideBanner);
        btnShowInter.onClick.AddListener(ShowInterstitial);
        btnShowRewards.onClick.AddListener(ShowRewards);
        txtLog.text = "test";
    }

    [System.Obsolete]
    private void InitData() {
        AdsManagerWrapper.INSTANCE.Initialize((status) => {

        });
        AdsManagerWrapper.INSTANCE.LoadInterstitial();
        AdsManagerWrapper.INSTANCE.LoadRewards();
    }

    [System.Obsolete]
    private void ShowBanner() {
        AdsManagerWrapper.INSTANCE.ShowBanner( (onAdLoded) => {

        }, (onAdFailedToLoad) => {
            Debug.Log("loadBanner : "+onAdFailedToLoad);
            txtLog.text = onAdFailedToLoad;
        });
    }

    [System.Obsolete]
    private void HideBanner() {
        AdsManagerWrapper.INSTANCE.HideBanner();
    }

    [System.Obsolete]
    private void ShowInterstitial() {
        AdsManagerWrapper.INSTANCE.ShowInterstitial( (onAdLoded) => {

        }, (onAdFailedToLoad) => {
            Debug.Log("loadIntersitial : "+onAdFailedToLoad);
            txtLog.text = onAdFailedToLoad;
        });
    }
    private void ShowRewards() {
        AdsManagerWrapper.INSTANCE.ShowRewards( (onAdLoded) => {

        }, (onAdFailedToLoad) => {
            Debug.Log("load Reards : "+onAdFailedToLoad);
            txtLog.text = onAdFailedToLoad;
        }, (iRewards) => {
            Debug.Log("rewards bro: "+iRewards.rewardsItem.Amount);
        });
    }

}
