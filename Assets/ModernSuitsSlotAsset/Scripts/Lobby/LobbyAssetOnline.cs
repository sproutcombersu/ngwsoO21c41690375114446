using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyAssetOnline : MonoBehaviour
{
    public Image imgLobby;
    // Start is called before the first frame update
    void Awake()
    {
        if(EASettingManager.instance && imgLobby && EASettingManager.instance.spriteMenu) {
            imgLobby.sprite = EASettingManager.instance.spriteMenu;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
