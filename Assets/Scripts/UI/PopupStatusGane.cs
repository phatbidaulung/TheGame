using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupStatusGane : UIBase
{
    [SerializeField] private TMP_Text _textStatusGame;
    [SerializeField] private TMP_Text _textButton;
    [SerializeField] private Button _btnHome;
    [SerializeField] private Button _btnResetScene;

    [SerializeField] private ELevel _levelThisScene;

    private void OnEnable() {
        _textStatusGame.text = GameManager.Instance.StatusGameIs().ToString();
    }
}
