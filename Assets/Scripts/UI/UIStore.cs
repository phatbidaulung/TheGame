using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : UIManager
{
   [SerializeField] private Button _buttonBack;
   private void Start() 
   {
        this._buttonBack.onClick.AddListener(() => ClosePopup(this.gameObject));
   }
}
