using System;
using System.Collections;
using System.Threading.Tasks;
using EaAds;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public static  class EAUtils
{

    public static string nextLevel;
    public static async Task<Sprite> LoadSpriteFromURL(string url, Action<DataProgress> progresss)
    {
        var dataProgress = new DataProgress();
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            uwr.SendWebRequest();

            while (!uwr.isDone)
            {
                var progresssPersen = string.Format("{0:0}%", uwr.downloadProgress * 100);

                dataProgress.progressPersen = progresssPersen;
                dataProgress.progressFloat = uwr.downloadProgress;

                progresss.Invoke(dataProgress);
                await Task.Delay(100);  // Tunggu sedikit sebelum memeriksa lagi.
            }

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
                throw new System.Exception("Sprite download failed");
            }
            else
            {
                
                dataProgress.progressPersen = "100%";
                dataProgress.progressFloat = 10;

                progresss.Invoke(dataProgress);
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                return sprite;
            }
        }
    }

    public static async Task<GameObject> DownloadAssetBundle(string bundleUrl, string assetName, Action<DataProgress> progresss)
    {
        var dataProgress = new DataProgress();
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl))
        {
            uwr.SendWebRequest();

            while (!uwr.isDone)
            {
                var progresssPersen = string.Format("{0:0}%", uwr.downloadProgress * 100);
                dataProgress.progressPersen = progresssPersen;
                dataProgress.progressFloat = uwr.downloadProgress;

                progresss.Invoke(dataProgress);
                await Task.Delay(100); // Tunggu sebentar sebelum memeriksa lagi.
            }

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(uwr.error);
                throw new System.Exception("AssetBundle download failed");
            }
            else
            {
                dataProgress.progressPersen = "100%";
                dataProgress.progressFloat = 10;

                progresss.Invoke(dataProgress);
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                GameObject obj = bundle.LoadAsset<GameObject>(assetName);
                return obj;
            }
        }
    }

    public static IEnumerator LoadAssetBundle(
        string bundleUrl,
        string assetName,
            Action<DataProgress> progresss,
            Action<GameObject, bool> result
            )
    {

        var dataProgress = new DataProgress();
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        www.SendWebRequest();

        while (!www.isDone)
        {
            Debug.Log("progress" + www.downloadProgress);
            var progresssPersen = string.Format("{0:0}%", www.downloadProgress * 100);
            dataProgress.progressPersen = progresssPersen;
            dataProgress.progressFloat = www.downloadProgress;

            progresss.Invoke(dataProgress);
            yield return null;
        }

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            result(null, false);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            GameObject obj = bundle.LoadAsset<GameObject>(assetName);
            result(null, false);
        }
    }

    public static async Task<Settings> GetSettingFromServer(string url, Action<DataProgress> progresss)
    {
        var dataProgress = new DataProgress();
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            // Send request and wait
            www.SendWebRequest();

            // While the request is still processing...
            while (!www.isDone)
            {
                var progresssPersen = string.Format("{0:0}%", www.downloadProgress * 100);

                dataProgress.progressPersen = progresssPersen;
                dataProgress.progressFloat = www.downloadProgress;

                progresss.Invoke(dataProgress);
                await Task.Delay(100);  // Tunggu sedikit sebelum memeriksa lagi.
            }

            // If there are network errors
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                return null;
            }
            else
            {
                // Parse JSON response
                var root = JsonUtility.FromJson<RootEASettingModel>(www.downloadHandler.text);
                return root.Settings;
            }
        }
    }

    public static async Task<AudioClip> LoadAudioFromUrl(string url, Action<DataProgress> progresss)
    {
        var dataProgress = new DataProgress();
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            www.SendWebRequest();

            while (!www.isDone)
            {
                var progresssPersen = string.Format("{0:0}%", www.downloadProgress * 100);

                dataProgress.progressPersen = progresssPersen;
                dataProgress.progressFloat = www.downloadProgress;

                progresss.Invoke(dataProgress);
                await Task.Delay(100);
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                return null;
            }
            else
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);
                return audioClip;
            }
        }
    }

}
