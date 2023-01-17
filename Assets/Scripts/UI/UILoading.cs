using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Ensign;
using Ensign.Unity;
using Ensign.Tween;

public class UILoading : UIManager
{
    private const string VALUE_FORMAT = "{0}%";
    [SerializeField] private Slider _sliderLoading;
    [SerializeField] private TextMeshProUGUI _textLoadingValue;
    [SerializeField] private CanvasGroup _loading;

    [SerializeField] private float _timeDelayTurnOffObject = 1.5f;
    
    private float _valueSlider = 0;

    private void OnEnable() 
    {
        _valueSlider = 0f;
        _loading.alpha = 1f;
    }

    private void FixedUpdate() 
    {
        if(_valueSlider < _sliderLoading.maxValue)
            LoadingRun();
        else
        {
            ChangeAlpha(_loading, 0f, _timeDelayTurnOffObject);
            this.ActionWaitTime(_timeDelayTurnOffObject, () =>
            {
                ClosePopup(this.gameObject);
            });
        }
    }
    private void LoadingRun()
    {
        _valueSlider += 0.5f;
        _sliderLoading.value = _valueSlider;
        _textLoadingValue.text =  string.Format(VALUE_FORMAT, (int)_valueSlider);
    }
}
