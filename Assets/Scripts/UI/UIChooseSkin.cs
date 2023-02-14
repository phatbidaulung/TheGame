using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Ensign.Unity;

public class UIChooseSkin : UIBase
{
    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonLeft;
    [SerializeField] private Button _buttonRight;

    [SerializeField] private SkinDataBase _skinDB;
    [SerializeField] private Image _skinImage;
    [SerializeField] private TextMeshProUGUI _skinName;

    [SerializeField] private CanvasGroup _choosseSkin;

    [SerializeField] private float _timeDelayTurnOffObject = 0.5f;
    private int _selectSkin = 0;

   private void Awake() {
        _choosseSkin.alpha = 0f;
   }
    private void Start() 
    {
        _selectSkin = PlayerPrefs.GetInt("selectSkin");
        this._buttonBack.onClick.AddListener(TurnOffObject);
        this._buttonLeft.onClick.AddListener(LastSkin);
        this._buttonRight.onClick.AddListener(NextSkin);
        UpdateSkin(_selectSkin);
    }
    private void OnEnable() 
    {
        ChangeAlpha(_choosseSkin, 1f, _timeDelayTurnOffObject);
    }
    private void LastSkin()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        _selectSkin--;
        if(_selectSkin < 0)
        {
            _selectSkin = _skinDB.SkinsCount - 1;
        }
        CheckPurchasedSkins(0);
    }
    private void NextSkin()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        _selectSkin++;
        if(_selectSkin >= _skinDB.SkinsCount)
        {
            _selectSkin = 0;
        }
        CheckPurchasedSkins(1);
    }
    private void CheckPurchasedSkins(int index)
    {
        Skins _skins = _skinDB.GetSkins(_selectSkin);
        if(_skins.Buy == false)
        {
            if(index == 0){
                LastSkin();
            }
            if(index == 1){
                NextSkin();
            }
        }
        else{
            UpdateSkin(_selectSkin);
        }
    }
    private void UpdateSkin(int index)
    {
        Skins _skins = _skinDB.GetSkins(index); 
        _skinImage.sprite = _skins.SkinSprite;
        _skinName.text = _skins.SkinName;
        PlayerPrefs.SetInt("selectSkin", _selectSkin);
    }
    private void TurnOffObject()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        ChangeAlpha(_choosseSkin, 0f, _timeDelayTurnOffObject);
        this.ActionWaitTime(_timeDelayTurnOffObject, () =>
        {
            ClosePopup(this.gameObject);
        });
    }
}
