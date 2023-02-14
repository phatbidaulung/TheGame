using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class BuySkin : MonoBehaviour
{
    [SerializeField] private SkinDataBase _skinDB;
    [SerializeField] private Button _btnBuy;
    [SerializeField] private TMP_Text _textStatusButton;
    [SerializeField] private TMP_Text _nameSkin;
    [SerializeField] private Image _skinImage;
    [SerializeField] private int _numberSkin;
    private void OnEnable() {
        this._btnBuy.onClick.AddListener(Buy);
        SetUp();
    }
    private void SetUp()
    {
        if((_numberSkin < _skinDB.SkinsCount) && (_numberSkin >= 0))
        {
            Skins _skins = _skinDB.GetSkins(_numberSkin);
            _nameSkin.text = _skins.SkinName;
            _skinImage.sprite = _skins.SkinSprite;

            if(_skins.Buy == true){
                _textStatusButton.text = "Đã sở hữu";
                _btnBuy.interactable = false;
            } 
        }
        else{
            _nameSkin.text = "null";
            _btnBuy.interactable = false;
        }
    }
    private void Buy()
    {
        if(_numberSkin <= _skinDB.SkinsCount)
        {
            Skins _skins = _skinDB.GetSkins(_numberSkin);
            _skins.Buy = true;
            SetUp();
        }
    }
}
