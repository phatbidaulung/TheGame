using UnityEngine;
using UnityEngine.UI;

using Ensign.Unity;
public class UIStore : UIBase
{
     [SerializeField] private Button _buttonBack;
     [SerializeField] private CanvasGroup _store;

     [SerializeField] private float _timeDelayTurnOffObject = 0.5f;
     private void Awake() {
          _store.alpha = 0f;
     }
     private void Start() 
     {
          this._buttonBack.onClick.AddListener(TurnOffObject);
     }
     private void OnEnable() 
     {
          ChangeAlpha(_store, 1f, _timeDelayTurnOffObject);
     }
     private void TurnOffObject()
     {
          SoundManager.Instance.PlaySound(EActionSound.Button);
          ChangeAlpha(_store, 0f, _timeDelayTurnOffObject);
          this.ActionWaitTime(_timeDelayTurnOffObject, () =>
          {
               ClosePopup(this.gameObject);
          });
     }
}
