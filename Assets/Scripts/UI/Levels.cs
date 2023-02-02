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
    private void OnEnable() 
    {
        this._level.onClick.AddListener(ButtonLevel);
        _textLevel.text = _indexLevel.ToString();
    }
    private void ButtonLevel()
    {
        SoundManager.Instance.PlaySound(EActionSound.Button);
        LoadScenes(_indexLevel.ToString());
    }
}
