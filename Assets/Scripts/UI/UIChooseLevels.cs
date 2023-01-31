using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Ensign.Unity;
public class UIChooseLevels : UIBase
{
    [SerializeField] private Button _buttonClose;
   [SerializeField] private CanvasGroup _chooseLevel;

   [SerializeField] private float _timeDelayTurnOffObject = 0.5f;
   private void Awake() {
        _chooseLevel.alpha = 0f;
   }
    private void OnEnable() 
    {
        ChangeAlpha(_chooseLevel, 1f, _timeDelayTurnOffObject);
    }
    private void Start() 
    {
        this._buttonClose.onClick.AddListener(TurnOffObject);
    }
   private void TurnOffObject()
   {
        ChangeAlpha(_chooseLevel, 0f, _timeDelayTurnOffObject);
        this.ActionWaitTime(_timeDelayTurnOffObject, () =>
        {
            ClosePopup(this.gameObject);
        });
   }
}
