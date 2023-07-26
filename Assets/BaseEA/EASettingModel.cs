using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{
    public string urlBgMenu;
    public string urlBgGamePlay;
    public string urlSoundBgGame;
}

[System.Serializable]
public class RootEASettingModel
{
    public Settings Settings;
}