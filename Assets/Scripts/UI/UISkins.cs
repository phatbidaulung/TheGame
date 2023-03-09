using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Ensign.Unity;
public class UISkins : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button _btnSkin;
    
    [Header("Skin")]
    [SerializeField] private SkinDataBase _skinDB;
    [SerializeField] private Transform _parentSkin;
    [SerializeField] private TMP_Text _indexSkin;
    [SerializeField] private TMP_Text _nameSkin;

    private void OnEnable() 
    {
        this.ActionWaitTime(0.01f, () => {
            UpdateSkin();
        });
        _btnSkin.onClick.AddListener(SelectSkin);
    }
    private void UpdateSkin()
    {
        if(_nameSkin.text == "")
        {
            Skins _skins = _skinDB.GetSkins(Int32.Parse(_indexSkin.text)); 
            // Layer "UI"
            _skins.skin.layer = 5;
            Instantiate(_skins.skin, _parentSkin.transform.position, Quaternion.identity, _parentSkin);
            _nameSkin.text = _skins.skinName;
        }
    }
    private void SelectSkin()
    {
        PlayerPrefs.SetInt("selectSkin", Int32.Parse(_indexSkin.text));
        UIManager.Instance.ClosePopup(EActionUI.UIChooseSkins);
    }
}
