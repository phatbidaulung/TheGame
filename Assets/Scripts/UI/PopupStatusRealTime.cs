using UnityEngine;
using TMPro;

using Ensign.Unity;
public class PopupStatusRealTime : UIBase
{
    [SerializeField] private TMP_Text _textStatus;
    [SerializeField] private CanvasGroup _background;
    [SerializeField] private float _timeDelayTurnOffObject = 1f;
    private void OnEnable() 
    {
        _background.alpha = 1f;
        ChangeAlpha(_background, 0f, _timeDelayTurnOffObject);
        this.ActionWaitTime(_timeDelayTurnOffObject, () =>
        {
            ClosePopup(this.gameObject);
        });
    }
    public void ChangeText(string input)
    {
        _textStatus.text = input;
    }
}