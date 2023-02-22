using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Ensign.Unity;

public class RemoveAds : UIBase
{
    [SerializeField] private TMP_InputField _inputCode;
    [SerializeField] private TMP_Text _textStatus;
    [SerializeField] private Button _btnSubmit;
    [SerializeField] private Button _btnClose;
    [SerializeField] private GameObject _popupRemoveAd;
    [SerializeField] private CanvasGroup _background;
    [SerializeField] private float _timeDelayTurnOffObject = 0.5f;
    public static readonly string codeRemoveAd = "UNICORNSTUDIOGAME";

    private void Awake() 
    {
        _popupRemoveAd.transform.localScale = new Vector3(0f, 0f, 0f);
        _background.alpha = 0f;
    }
    private void Start()
    {
        IngameData.Instance.IsShowAds = Convert.ToBoolean(PlayerPrefs.GetInt("removeAds"));
        _btnSubmit.onClick.AddListener(CheckCodeRemoveAd);
        _btnClose.onClick.AddListener(TurnOffObject);
    }
    private void OnEnable() 
    {
        // Turn on animation
        ChangeScale(_popupRemoveAd, new Vector3(1f, 1f, 1f), _timeDelayTurnOffObject);
        ChangeAlpha(_background, 1f, _timeDelayTurnOffObject);
    }
    private void CheckCodeRemoveAd()
    {
        if(_inputCode.text.ToUpper() == codeRemoveAd)
        {
            PlayerPrefs.SetInt("removeAds", 1);
            IngameData.Instance.IsShowAds = Convert.ToBoolean(PlayerPrefs.GetInt("removeAds"));
            _textStatus.text = "Remove Ads success";
            AdmobController.Instance.ShowBanner(false);
            Debug.Log($"Remove ads suceess {IngameData.Instance.IsShowAds}");
        }
        else
        {
            _textStatus.text = "Please try again";
        }
    }
    private void TurnOffObject()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        ChangeAlpha(_background, 0f, _timeDelayTurnOffObject);
        ChangeScale(_popupRemoveAd, new Vector3(0f, 0f, 0f), _timeDelayTurnOffObject);
        this.ActionWaitTime(_timeDelayTurnOffObject, () =>
        {
            ClosePopup(this.gameObject);
        });
    }

}
