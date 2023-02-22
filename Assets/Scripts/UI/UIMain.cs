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
    [SerializeField] private Button _btnRemoveAd;

    [SerializeField] private UIManager _uiManager;

    private void Start() {
        this._btnSetting.onClick.AddListener(ButtonSetting);
        this._btnChosseSkins.onClick.AddListener(ButtonChooseSkin);
        this._btnStore.onClick.AddListener(ButtonStore);
        this._btnNormal.onClick.AddListener(ButtonNormalMode);
        this._btnEndless.onClick.AddListener(ButtonEndlessMode);
        if(_btnRemoveAd != null)
            this._btnRemoveAd.onClick.AddListener(ButtonRemoveAd);
    }

    private void ButtonSetting()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        _uiManager.OpenPopup(EActionUI.PopupSetting);
    }
    private void ButtonChooseSkin()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        _uiManager.OpenPopup(EActionUI.UIChooseSkins);
    }
    private void ButtonStore()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        _uiManager.OpenPopup(EActionUI.UIStore);
    }
    private void ButtonNormalMode()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        _uiManager.OpenPopup(EActionUI.UIChooseLevels);
    }
    private void ButtonEndlessMode()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        _uiManager.LoadSceneWithLoadingPopup(_endless);
    }
    private void ButtonRemoveAd()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        _uiManager.OpenPopup(EActionUI.UIRemoveAd);
    }
}
