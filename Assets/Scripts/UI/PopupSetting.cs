using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Ensign.Unity;
public class PopupSetting : UIBase
{
    [SerializeField] private Button _btnClose;
    [SerializeField] private TMP_Dropdown _changeMovePlayer;
    [SerializeField] private Slider _sliderVolume;
    [SerializeField] private GameObject _popupSetting;
    [SerializeField] private CanvasGroup _background;
    [SerializeField] private float _timeDelayTurnOffObject = 0.5f;
    private void Awake() {
        _popupSetting.transform.localScale = new Vector3(0f, 0f, 0f);
        _background.alpha = 0f;
    }
    private void OnEnable() {
        // Turn on animation
        ChangeScale(_popupSetting, new Vector3(1f, 1f, 1f), _timeDelayTurnOffObject);
        ChangeAlpha(_background, 1f, _timeDelayTurnOffObject);
    }
    private void Start() {
        this._btnClose.onClick.AddListener(TurnOffObject);
        LoadVolume();
    }
    public void ChangeVolume() 
    { 
        SoundManager.volume = _sliderVolume.value; 
        SoundManager.Instance.SaveVolume();
    }  
    private void LoadVolume() { _sliderVolume.value = SoundManager.volume; }

    private void TurnOffObject()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        ChangeAlpha(_background, 0f, _timeDelayTurnOffObject);
        ChangeScale(_popupSetting, new Vector3(0f, 0f, 0f), _timeDelayTurnOffObject);
        this.ActionWaitTime(_timeDelayTurnOffObject, () =>
        {
            ClosePopup(this.gameObject);
        });
    }

    public void ChangeTypeMovement(int index)
    {
        switch (index)
        {
            case 0:
                GameManager.Instance.ChangeControllerPlayer(EControl.CrossyRoad);
                break;
            case 1:
                GameManager.Instance.ChangeControllerPlayer(EControl.FPS);
                break;
            case 2:
                GameManager.Instance.ChangeControllerPlayer(EControl.TrafficRoad);
                break;
        }
    }
}
