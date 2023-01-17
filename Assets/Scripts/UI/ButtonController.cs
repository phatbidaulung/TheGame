using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private EButtonController _typeButton;
    [SerializeField] private Button _buttonController;

    private void Start() {
        this._buttonController.onClick.AddListener(() => Log(_typeButton));
    }
    
    private void Log(EButtonController type)
    {
        Debug.Log(type);
    }

    private void Update() {
        InputButtom();
    }
    private void InputButtom()
    {
        //Top
        if(Input.GetKeyDown(KeyCode.W)){
            Log(EButtonController.ButtonTop);
        }
        //Bottom
        if(Input.GetKeyDown(KeyCode.S)){
            Log(EButtonController.ButtonBottom);
        }
        //Left
        if(Input.GetKeyDown(KeyCode.A)){
            Log(EButtonController.ButtonLeft);
        }
        //Right
        if(Input.GetKeyDown(KeyCode.D)){
            Log(EButtonController.ButtonRight);
        }
    }
}

public enum EButtonController
{
    ButtonLeft,
    ButtonRight,
    ButtonTop,
    ButtonBottom
}