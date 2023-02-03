using UnityEngine;

using TMPro;

public class UIScore : UIBase
{
    [SerializeField] private TMP_Text _score;
    public void ChangeScoreInScreen(string input)
    {
        this._score.text = input;
    }
}
