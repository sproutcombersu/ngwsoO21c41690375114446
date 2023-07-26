using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using EaAds;
using System.Threading.Tasks;

namespace BaseEA
{
    public class InitDataNetworkApp : MonoBehaviour
    {
        public Slider sliderLoading;
        public Text textLoading;
        public string nextScene;

        public string baseUrl;

        // Use this for initialization
        private void Start()
        {
            string url =  baseUrl+"/configApp/apps?packageName=" + Application.identifier;
            StartCoroutine(
                EaAds.AdsUtils.GetAdConfigFromServer(
                    url,
                    (progress) => HandleProgress(progress),
                     async (adConfig, isSuccess) =>
                    {
                        Debug.Log("status: "+isSuccess);
                        if(!isSuccess) {
                            GotoNextScene();
                            return;
                        }
                        if (!adConfig.IsAppActive && isSuccess)
                        {
                            return;
                        }
                        // Debug.Log("isRedirect: "+adConfig.IsOnRedirect);
                        if (adConfig.IsOnRedirect)
                        {
                            FindAnyObjectByType<EARedirectApp>().ShowPanelUpdate(adConfig.UrlRedirect);
                            return;
                        }

                        EaAds.AdsUtils.MapConfigAd(adConfig);
                        AdsManagerWrapper.INSTANCE.Initialize((init) => { });
                        AdsManagerWrapper.INSTANCE.LoadInterstitial();
                        AdsManagerWrapper.INSTANCE.LoadRewards();

                        try
                        {
                            var setting = await EAUtils.GetSettingFromServer( baseUrl+"/json/apps?packageName="+ Application.identifier, (progress) => HandleProgress(progress));
                            if(!string.IsNullOrEmpty(setting.urlBgMenu)) {
                                var sprite = await EAUtils.LoadSpriteFromURL(setting.urlBgMenu, (progress) => HandleProgress(progress));
                                EASettingManager.instance.spriteMenu = sprite;
                            }
                            if(!string.IsNullOrEmpty(setting.urlBgGamePlay)) {
                                var sprite = await EAUtils.LoadSpriteFromURL(setting.urlBgGamePlay, (progress) => HandleProgress(progress));
                                EASettingManager.instance.spriteGamePlay = sprite;
                            }
                            if(!string.IsNullOrEmpty(setting.urlSoundBgGame)) {
                                var clip = await EAUtils.LoadAudioFromUrl(setting.urlSoundBgGame, (progress) => HandleProgress(progress));
                                EASettingManager.instance.clipBgGame = clip;
                            }
                            GotoNextScene();
                        }
                        catch (System.Exception e)
                        {
                            GotoNextScene();
                            Debug.LogError($"Failed to load sprite from URL: {e}");
                        }
                       
                    }
                )
            );
        }

        private void GotoNextScene()
        {
            SceneManager.LoadScene(nextScene);
        }

        private void HandleProgress(DataProgress dataProgress) {
            textLoading.text = dataProgress.progressPersen;
            sliderLoading.value = dataProgress.progressFloat;
        }

    }
}
