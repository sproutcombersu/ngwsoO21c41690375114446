using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EALoadingShowInt : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        // if(AdsManagerWrapper.INSTANCE) {
        //     AdsManagerWrapper.INSTANCE.ShowInterstitial((callbackAds) => {
        //         if(!string.IsNullOrEmpty(callbackAds.ErrorMessage)) {
        //             LoadNextScene();
        //         }
        //         if(!string.IsNullOrEmpty(callbackAds.LoadMessage)){
        //             LoadNextScene();
        //         }
        //     });
        // } else {
        //      LoadNextScene();
        // }
    }

    private void LoadNextScene() {
        SceneManager.LoadScene (EAUtils.nextLevel);
    }
}
