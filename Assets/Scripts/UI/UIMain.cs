using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : UIManager
{
    [SerializeField] private Button _btnSetting;
    [SerializeField] private Button _btnEndless;
    [SerializeField] private Button _btnNormal;
    [SerializeField] private Button _btnStore;
    [SerializeField] private Button _btnChosseSkins;

    private void Start() {
        this._btnSetting.onClick.AddListener(() => OpenPopup(_popupSetting));
        this._btnChosseSkins.onClick.AddListener(() => OpenPopup(_uiChooseSkins));
    }
}
