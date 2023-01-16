using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChooseLevels : UIManager
{
    [SerializeField] private Button _buttonClose;
    private void Start() 
    {
        this._buttonClose.onClick.AddListener(() => ClosePopup(this.gameObject));
    }
}
