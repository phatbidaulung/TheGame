using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PopupSetting : UIManager
{
    [SerializeField] private Button _btnClose;
    [SerializeField] private Slider _sliderVolume;

    private void Start() {
        this._btnClose.onClick.AddListener(() => ClosePopup(_popupSetting));
        LoadVolume();
    }
    public void ChangeVolume() 
    { 
        SoundManager.volume = _sliderVolume.value; 
        SoundManager.Instance.SaveVolume();
    }  
    private void LoadVolume() { _sliderVolume.value = SoundManager.volume; }
}
