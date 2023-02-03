using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private EButtonController _typeButton;
    [SerializeField] private Button _buttonController;
    [SerializeField] private PlayerView _playerView;

    private void Start() {
        this._buttonController.onClick.AddListener(() => MoveWithTouch(_typeButton));
    }
    
    private void MoveWithTouch(EButtonController type)
    {
        switch (type)
        {
            case EButtonController.ButtonTop:
                _playerView.MoveToTop();
                break;
            case EButtonController.ButtonBottom:
                _playerView.MoveToBottom();
                break;
            case EButtonController.ButtonLeft:
                _playerView.MoveToLeft();
                break;
            case EButtonController.ButtonRight:
                _playerView.MoveToRight();
                break;
        }
    }

    private void Update() {
    }
}

public enum EButtonController
{
    ButtonLeft,
    ButtonRight,
    ButtonTop,
    ButtonBottom
}