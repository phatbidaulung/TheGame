using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ensign.Unity;
public class IngameData : Singleton<IngameData>
{
    private bool _isShowAds;
    public bool IsShowAds
    {
        get => _isShowAds;
        set => _isShowAds = value;
    }
    private void Awake() {
        _isShowAds = Convert.ToBoolean(PlayerPrefs.GetInt("removeAds"));
        DontDestroyOnLoad(this.gameObject);
    }
}