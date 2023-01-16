using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UIChooseSkin : UIManager
{
    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonLeft;
    [SerializeField] private Button _buttonRight;

    [SerializeField] private SkinDataBase _skinDB;
    [SerializeField] private Image _skinImage;
    [SerializeField] private TextMeshProUGUI _skinName;
    private int _selectSkin = 0;

    private void Start() 
    {
        _selectSkin = PlayerPrefs.GetInt("selectSkin");
        this._buttonBack.onClick.AddListener(() => ClosePopup(this.gameObject));
        this._buttonLeft.onClick.AddListener(LastSkin);
        this._buttonRight.onClick.AddListener(NextSkin);
        UpdateSkin(_selectSkin);
    }
    private void LastSkin()
    {
        _selectSkin--;
        if(_selectSkin < 0)
        {
            _selectSkin = _skinDB.SkinsCount - 1;
        }
        UpdateSkin(_selectSkin);
    }
    private void NextSkin()
    {
        _selectSkin++;
        if(_selectSkin >= _skinDB.SkinsCount)
        {
            _selectSkin = 0;
        }
        UpdateSkin(_selectSkin);
    }
    private void UpdateSkin(int index)
    {
        Skins _skins = _skinDB.GetSkins(index);
        _skinImage.sprite = _skins.SkinSprite;
        _skinName.text = _skins.SkinName;
        PlayerPrefs.SetInt("selectSkin", _selectSkin);
    }
}
