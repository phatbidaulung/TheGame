using UnityEngine;

public class UIManager : UIBase
{
    public static UIManager Instance;
    [SerializeField] private UIScore _uiScore;
    [SerializeField] private UILoading _loading;
    
    [Space, Header("Popups")]
    [SerializeField] private GameObject _popupSetting;
    [SerializeField] private GameObject _popupStatusGame;
    [SerializeField] private GameObject _popupStatusRealTime;
    [SerializeField] private GameObject _popupRemoveAd;
    [SerializeField] private GameObject _uiChooseSkins;
    [SerializeField] private GameObject _uiStore;
    [SerializeField] private GameObject _uiChooseLevels;
    
    private void Awake()
    {
        Instance = this;
    }
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
            case EActionUI.UIRemoveAd:
                base.OpenPopup(_popupRemoveAd);
                break;
        }
    }

    public void LoadSceneWithLoadingPopup(string nameScene)
    {
        _loading.LoadScene(nameScene);
    }
}

public enum EActionUI
{
    PopupSetting,
    PopupStatusRealTime,
    UIChooseSkins,
    UIChooseLevels,
    UIStore,
    UIRemoveAd
}