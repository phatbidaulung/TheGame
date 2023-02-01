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

    [SerializeField] private UIManager _uiManager;

    private void Start() {
        this._btnSetting.onClick.AddListener(() => _uiManager.OpenPopup(EActionUI.PopupSetting));
        this._btnChosseSkins.onClick.AddListener(() => _uiManager.OpenPopup(EActionUI.UIChooseSkins));
        this._btnStore.onClick.AddListener(() => _uiManager.OpenPopup(EActionUI.UIStore));
        this._btnNormal.onClick.AddListener(() => _uiManager.OpenPopup(EActionUI.UIChooseLevels));
        this._btnEndless.onClick.AddListener(() => LoadScenes(_endless));
    }
}
