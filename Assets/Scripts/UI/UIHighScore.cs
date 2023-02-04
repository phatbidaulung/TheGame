using UnityEngine;
using System.Linq;
using TMPro;

using Ensign.Unity;
public class UIHighScore : UIBase
{
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CanvasGroup _background;
    [SerializeField] private float _timeDelayTurnOffObject = 1f;
    [SerializeField] private float _timeChangeAlpha = 1f;
    private void OnEnable() {
        _text.text = "High score: " + GetHighScoreFromFile().ToString();
        _background.alpha = 1f;
        this.ActionWaitTime(_timeDelayTurnOffObject, () => 
        {
            ChangeAlpha(_background, 0f, _timeChangeAlpha);
            this.ActionWaitTime(_timeChangeAlpha, () =>
            {
                ClosePopup(this.gameObject);
            });
        });
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
