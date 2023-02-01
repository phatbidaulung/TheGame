using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupStatusGane : UIBase
{
    [Space, Header("Objects")]
    [SerializeField] private TMP_Text _textStatusGame;
    [SerializeField] private Button _btnHome;
    [SerializeField] private Button _btnResetScene;

    [Space, Header("Popup Status Game")]
    [SerializeField] private GameObject _popupStatusGame;
    [SerializeField] private CanvasGroup _background;
    [SerializeField] private float _timeDelayTurnOffObject = 0.5f;
    
    [Space, Header("UI Manager")]
    [SerializeField] private UIManager _uiManager;

    private void Awake() {
        // Prepare for animation 
        _popupStatusGame.transform.localScale = new Vector3(0f, 0f, 0f);
        _background.alpha = 0f;
    }
    private void OnEnable() {
        _btnResetScene.onClick.AddListener( () => LoadScenes(NameScene()));
        _btnHome.onClick.AddListener( () => LoadScenes("Main"));
        _textStatusGame.text = GameManager.Instance.StatusGameIs().ToString();

        // Turn on animation
        ChangeScale(_popupStatusGame, new Vector3(1f, 1f, 1f), _timeDelayTurnOffObject);
        ChangeAlpha(_background, 1f, _timeDelayTurnOffObject);
    }
    private void OnDisable() {
        _btnResetScene.onClick.RemoveAllListeners();
        _btnHome.onClick.RemoveAllListeners();
    }
}