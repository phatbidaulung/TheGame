using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

using Ensign.Unity;

public class UIChooseLevels : UIBase
{
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private Button _buttonClose;
    [SerializeField] private CanvasGroup _background;
    public List<GameObject> _finishLevels;
    [SerializeField] private float _timeDelayTurnOffObject = 0.5f;
    private void Awake() {
        // Prepare for animation 
        _background.alpha = 0f;
    }
    private void OnEnable() 
    {        
        ChangeAlpha(_background, 1f, _timeDelayTurnOffObject);
        CheckLevelFinish();
    }
    private void Start() 
    {
        this._buttonClose.onClick.AddListener(TurnOffObject);
    }
    private void TurnOffObject()
    {
        
        SoundManager.Instance.PlaySound(EActionSound.Button);
        ChangeAlpha(_background, 0f, _timeDelayTurnOffObject);
        this.ActionWaitTime(_timeDelayTurnOffObject, () =>
        {
            ClosePopup(this.gameObject);
        });
    }
    public void CheckLevelFinish()
    {
        //Get data from file
        ListModel _listData = _dataManager.ListModel();
        
        // Select level finish in normal mode
        var orderByResut = from n in _listData.PlayerModel
                           where n.TypeMap == ETypeMap.NormalMap && n.Finish == true
                           select n;
        var listLevel = orderByResut.ToArray();
        for(int i = 0; i < _finishLevels.Count; i++)
        {
            for(int j = 0; j < listLevel.Length; j++)
            {
                if(i == ((int)listLevel[j].LevelMap))
                {
                    _finishLevels[i].SetActive(true);
                }
            }
        }
    }
}