using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class Levels : UIManager
{
    private const string LEVEL_FORMAT = "Level {0}";
    [SerializeField] private Button _level;
    [SerializeField] private int _indexLevel;
    [SerializeField] private TextMeshProUGUI _textLevel;
    private void Start() {
        this._level.onClick.AddListener(() => ChangeLevel(_indexLevel));
        _textLevel.text = string.Format(LEVEL_FORMAT, _indexLevel);
    }

    private void ChangeLevel(int index)
    {
        LoadScenes(string.Format(LEVEL_FORMAT, index));
    }
}
