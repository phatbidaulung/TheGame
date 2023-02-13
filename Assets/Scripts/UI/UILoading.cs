using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

using Ensign.Unity;

public class UILoading : UIBase
{
    private const string VALUE_FORMAT = "{0}%";
    [SerializeField] private Slider _sliderLoading;
    [SerializeField] private TextMeshProUGUI _textLoadingValue;
    [SerializeField] private CanvasGroup _loading;
	[SerializeField] private GameObject _loadingPopup;
    [SerializeField] private float _timeDelayTurnOffObject = 1.5f;
    
    private float _valueSlider = 0;

    private void OnEnable() 
    {
        // _valueSlider = 0f;
        // _loading.alpha = 1f;
    }

    private void FixedUpdate() 
    {
        // if(_valueSlider < _sliderLoading.maxValue)
        //     LoadingRun();
        // else
        // {
        //     ChangeAlpha(_loading, 0f, _timeDelayTurnOffObject);
        //     this.ActionWaitTime(_timeDelayTurnOffObject, () =>
        //     {
        //         ClosePopup(this.gameObject);
        //     });
        // }
    }
    public void LoadScene(string nameScene)
    {
        StartCoroutine(LoadSceneAsync(nameScene));
    }
    IEnumerator LoadSceneAsync(string nameScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);
        _loadingPopup.SetActive(true);
        
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            _sliderLoading.value = progressValue;
            _textLoadingValue.text =  string.Format(VALUE_FORMAT, (int)(progressValue * 100));
            yield return null;
        }
    }
    private void LoadingRun()
    {
        _valueSlider += 0.5f;
        _sliderLoading.value = _valueSlider;
        _textLoadingValue.text =  string.Format(VALUE_FORMAT, (int)_valueSlider);
    }
}
