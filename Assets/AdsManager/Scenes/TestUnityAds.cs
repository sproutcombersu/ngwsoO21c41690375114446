using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUnityAds : MonoBehaviour
{
    public UnityAdsNetwork unityAdsNetwork;
    public Button btnInit;
    public Button btnShowBanner;
    // Start is called before the first frame update
    void Start()
    {
        if(!unityAdsNetwork) {
            unityAdsNetwork = gameObject.GetComponent<UnityAdsNetwork>();
        }
        btnInit.onClick.AddListener(HandleInit);
        btnShowBanner.onClick.AddListener(HandleShowBanner);
    }

    private void HandleInit() {
        List<string> names = new List<string>
        {
            "John",
            "Jane",
            "Bob",
            "Alice"
        };

        unityAdsNetwork.Initialize("5186441", (status) => {}, names);
    }

    private void HandleShowBanner() {
        // unityAdsNetwork.ShowBanner("Banner_Android", (callbackAds) => {}, AdsBannerPosition.BOTTOM_CENTER);
    }

   
}
