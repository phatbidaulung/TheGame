using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using TMPro;

using Ensign.Unity;

public class UIChooseSkin : UIBase
{
    [Header("Buttons")]
    [SerializeField] private Button _buttonBack;

    [Header("Skins")]
    [SerializeField] protected SkinDataBase _skinDB;
    [SerializeField] private TMP_Text _indexSkin;
    [SerializeField] private GameObject _skinSample;
    [SerializeField] private GameObject _listSkins;
    [SerializeField] private List<string> _listSkinsCreated;

    [Header("UI")]
    [SerializeField] private CanvasGroup _choosseSkin;

    [Header("Value")]
    [SerializeField] private float _timeDelayTurnOffObject = 0.5f;
    protected int _selectSkin = 0;

    private void Awake() 
    {
        _choosseSkin.alpha = 0f;
        if(_listSkinsCreated.Count == 0)
            _listSkinsCreated.Add(_skinDB.GetSkins(0).skinName);
    }
    private void Start() 
    {
        _selectSkin = PlayerPrefs.GetInt("selectSkin");
        this._buttonBack.onClick.AddListener(TurnOffObject);
    }
    private void OnEnable() 
    {
        CreateListSkin();
        ChangeAlpha(_choosseSkin, 1f, _timeDelayTurnOffObject);
    }
    private void CreateListSkin()
    {
        for(int i=1; i < _skinDB.SkinsCount; i++)
        {
            if(!_listSkinsCreated.Contains(_skinDB.GetSkins(i).skinName) && _skinDB.GetSkins(i).buy)
            {
                _indexSkin.text = ""+i;
                Instantiate(_skinSample, _skinSample.transform.position, Quaternion.identity, _listSkins.transform);
                
                _listSkinsCreated.Add(_skinDB.GetSkins(i).skinName);
            }
        }
        
        // reset value to default
        _indexSkin.text = "0";

        for(int i=0; i < _listSkins.transform.childCount; i++)
        {
            _listSkins.transform.GetChild(i).gameObject.SetActive(true);
        }
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
