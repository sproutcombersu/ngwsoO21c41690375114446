using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDialog : MonoBehaviour {

    public GameObject container;

    private void Start()
    {
        container.SetActive(ConfigApp.configAppModel.isAutoRedirect);
    }


    public void GotoUpdate()
    {
        Application.OpenURL(ConfigApp.configAppModel.urlRedirect);
    }
}
