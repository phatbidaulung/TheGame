using UnityEngine;
using System.Linq;

using TMPro;

public class UIScore : UIBase
{
    [SerializeField] private TMP_Text _scoreNow;
    [SerializeField] private TMP_Text _highScore;
    [SerializeField] private DataManager _dataManager;
    private void OnEnable() {
        _highScore.text = GetHighScoreFromFile().ToString();
    }
    public void ChangeScoreInScreen(string input)
    {
        this._scoreNow.text = input;
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
