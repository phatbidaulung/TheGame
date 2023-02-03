using UnityEngine;

public class UIManager : UIBase
{
    [SerializeField] private UIScore _uiScore;
    
    [Space, Header("Popups")]
    [SerializeField] private GameObject _popupSetting;
    [SerializeField] private GameObject _popupStatusGame;
    [SerializeField] private GameObject _popupStatusRealTime;
    [SerializeField] private GameObject _uiChooseSkins;
    [SerializeField] private GameObject _uiStore;
    [SerializeField] private GameObject _uiChooseLevels;
    
    ///<summary>
    ///GameOver or WinGame
    ///</summary>
    public void OpenPopupStatusGame()
    {
        OpenPopup(_popupStatusGame);
    }
    public void ChangeScore(string input)
    {
        _uiScore.ChangeScoreInScreen(input);
    }

    public void OpenPopup(EActionUI uiOrPopup)
    {
        switch (uiOrPopup)
        {
            case EActionUI.PopupSetting:
                base.OpenPopup(_popupSetting);
                break;
            case EActionUI.PopupStatusRealTime:
                base.OpenPopup(_popupStatusRealTime);
                break;
            case EActionUI.UIChooseSkins:
                base.OpenPopup(_uiChooseSkins);
                break;
            case EActionUI.UIChooseLevels:
                base.OpenPopup(_uiChooseLevels);
                break;
            case EActionUI.UIStore:
                base.OpenPopup(_uiStore);
                break;
        }
    }
}

public enum EActionUI
{
    PopupSetting,
    PopupStatusRealTime,
    UIChooseSkins,
    UIChooseLevels,
    UIStore
}