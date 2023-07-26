using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotAssetOnline : MonoBehaviour
{

    public Image spriteBgSlog;
    void Awake()
    {
        if(EASettingManager.instance && spriteBgSlog && EASettingManager.instance.spriteGamePlay) {
            spriteBgSlog.sprite = EASettingManager.instance.spriteGamePlay;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
