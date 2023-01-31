using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : UIBase
{
    private string _endless = "EndlessMode";
    [Space, Header("Buttons")]
    [SerializeField] private Button _btnSetting;
    [SerializeField] private Button _btnEndless;
    [SerializeField] private Button _btnNormal;
    [SerializeField] private Button _btnStore;
    [SerializeField] private Button _btnChosseSkins;
    
    [Space, Header("Popups")]
    [SerializeField] protected GameObject _popupSetting;
    [SerializeField] protected GameObject _uiChooseSkins;
    [SerializeField] protected GameObject _uiStore;
    [SerializeField] protected GameObject _uiChooseLevels;

    private void Start() {
        this._btnSetting.onClick.AddListener(() => OpenPopup(_popupSetting));
        this._btnChosseSkins.onClick.AddListener(() => OpenPopup(_uiChooseSkins));
        this._btnStore.onClick.AddListener(() => OpenPopup(_uiStore));
        this._btnNormal.onClick.AddListener(() => OpenPopup(_uiChooseLevels));
        this._btnEndless.onClick.AddListener(() => LoadScenes(_endless));
    }
}
