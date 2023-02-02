using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class Levels : UIBase
{
    [SerializeField] private Button _level;
    [SerializeField] private ELevel _indexLevel;
    [SerializeField] private TextMeshProUGUI _textLevel;
    [SerializeField] private GameObject _lock;
    private void OnEnable() 
    {
        this._level.onClick.AddListener(ButtonLevel);
        _textLevel.text = _indexLevel.ToString();
        if(_indexLevel == ELevel.Lock)
        {
            _lock.SetActive(true);
        }
    }
    private void ButtonLevel()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        LoadScenes(_indexLevel.ToString());
    }
}
