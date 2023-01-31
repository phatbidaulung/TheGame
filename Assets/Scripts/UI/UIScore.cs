using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UIScore : UIBase
{
    [SerializeField] private TMP_Text _score;
    public void ChangeScoreInScreen(string input)
    {
        this._score.text = input;
    }
}
