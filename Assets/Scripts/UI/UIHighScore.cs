using UnityEngine;
using System.Linq;
using TMPro;

using Ensign.Unity;
using Ensign.Tween;
public class UIHighScore : UIBase
{
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CanvasGroup _background;
    [SerializeField] private GameObject _popup; 
    private void OnEnable() {
        _text.text = "High score: " + GetHighScoreFromFile().ToString();
        _background.alpha = 1f;
        // this.ActionWaitTime(_timeDelayTurnOffObject, () => 
        // {
        //     ChangeAlpha(_background, 0f, _timeChangeAlpha);
        //     this.ActionWaitTime(_timeChangeAlpha, () =>
        //     {
        //         ClosePopup(this.gameObject);
        //     });
        // });
        MovePopup();
    }
    private void MovePopup()
    {
        LeanTween.moveLocal(_popup, new Vector3(0f, 270f, 0f), 0.5f).setDelay(1f).setEase(LeanTweenType.easeInOutCirc);
        LeanTween.scale(_popup, new Vector3(1f, 1f, 1f), 0.5f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(_popup, new Vector3(0.7f, 0.7f, 0.7f), 0.5f).setDelay(4.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(_popup, new Vector3(760, 450f, 0f), 0.5f).setDelay(6f).setEase(LeanTweenType.easeOutQuint);
    }
    private float GetHighScoreFromFile()
    {
        try
        {
            ListModel _listData = _dataManager.ListModel();

            var orderByResut = from n in _listData.PlayerModel
                            orderby n.Score descending
                            select n;
                    
            var listLevel = orderByResut.ToArray();

            return listLevel[0].Score;
        }
        catch
        {
            return 0;
        }
        
    }
}
