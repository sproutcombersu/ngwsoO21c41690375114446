using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EARedirectApp : MonoBehaviour
{
    public GameObject container;
    private string urlLaunch;

    // Start is called before the first frame update
    private void Start() {
        container.SetActive(false);
    }
   
    public void ShowPanelUpdate(string urlLaunch) {
        this.urlLaunch = urlLaunch;
        container.SetActive(true);
    }

    public void GotoRedirectApp() {
        Application.OpenURL(urlLaunch);
    }
}
