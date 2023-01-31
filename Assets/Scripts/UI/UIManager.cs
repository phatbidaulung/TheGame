using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : UIBase
{
    [SerializeField] private UIScore _uiScore;
    [SerializeField] private GameObject _popupStatusGame;
    public void OpenPopupStatusGame()
    {
        OpenPopup(_popupStatusGame);
    }
    public void ChangeScore(string input)
    {
        _uiScore.ChangeScoreInScreen(input);
    }
}
