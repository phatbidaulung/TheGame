using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Tween;

using Ensign.Unity.MVC;
public class PlayerController : Controller<PlayerController, PlayerModel>
{
    protected override void Init()
    {
        base.Init();
        this.ChangeModel(new PlayerModel
        {
            timeDelayRotate  = 0.2f,
            JumpForce           = 2f,
            TimeDelay           = 0.1f,
            LimitMapLeft        = 4f,
            LimitMapRight       = 16f,
            Speed               = 0.2f,
            NextPosition        = default,
            InPlane             = true,
            MAX_SWIPE_TIME      = 0.5f,
            MIN_SWIPE_DISTANCE  = 0.17f,
            SwipedRight         = false,
            SwipedLeft          = false,
            SwipedDown          = false,
            SwipedUp            = false,
            StartPos            = default,
            StartTime           = 0f,

            startTouchPosition = default,
            currentPosition = default,
            endTouchPosition = default,
            stopTouch = false,
            swipeRange = 50f,
            tapRange = 10
        });
    }
    public void CanMove(bool input)
    {
        this.Model.InPlane = input;
    }
    public void PreventPlayerTurning(Vector3 index)
    {
        // Deduction 2 because player can come back 3 times. with time 3rd game over
        if(index.x <= (GameManager.Instance.MaxPositionPlayer().x - 3))
        {
            GameManager.Instance.GameOver();
        }
    }
    public void Movement(GameObject taget, EMovement typeMove)
    {
        if(GameManager.Instance.StatusGameIs() != EStatusGame.GameOver && this.Model.InPlane)
        {
            switch (typeMove)
            {
                case EMovement.MoveToTop:
                    this.Model.NextPosition = taget.transform.position + new Vector3(1, 0, 0);
                    LeanTween.moveLocalX(taget.gameObject, this.Model.NextPosition.x, this.Model.Speed);
                    Jump(taget.gameObject);
                    RotatePlayer(taget, 0);
                    // GameManager.Instance.IncreaseScore();
                    break;
                case EMovement.MoveToBottom:
                    this.Model.NextPosition = taget.transform.position - new Vector3(1, 0, 0);
                    LeanTween.moveLocalX(taget.gameObject, this.Model.NextPosition.x, this.Model.Speed);
                    Jump(taget.gameObject);
                    RotatePlayer(taget, 180);
                    UIManager.Instance.OpenPopup(EActionUI.PopupStatusRealTime);
                    break;
                case EMovement.MoveToLeft:
                    this.Model.NextPosition = taget.transform.position + new Vector3(0, 0, 1);
                    LeanTween.moveLocalZ(taget.gameObject, this.Model.NextPosition.z, this.Model.Speed);
                    Jump(taget.gameObject);
                    RotatePlayer(taget, -90);
                    break;
                case EMovement.MoveToRight:
                    this.Model.NextPosition = taget.transform.position - new Vector3(0, 0, 1);
                    LeanTween.moveLocalZ(taget.gameObject, this.Model.NextPosition.z, this.Model.Speed);
                    Jump(taget.gameObject);
                    RotatePlayer(taget, 90);
                    break;
                
            }

            PreventPlayerTurning(taget.transform.position);
            SoundManager.Instance.PlaySound(EActionSound.PlayerMove);
        }
    }
    public void Jump(GameObject taget)
    {
        LeanTween.moveLocalY(taget, taget.transform.position.y + this.Model.JumpForce, this.Model.Speed / 2).setEase(LeanTweenType.easeOutQuart);
        LeanTween.scale(taget, new Vector3(taget.transform.localScale.x + 0.1f, taget.transform.localScale.y + 0.1f, taget.transform.localScale.z + 0.1f), this.Model.Speed / 2);
        LeanTween.moveLocalY(taget, taget.transform.position.y, this.Model.Speed / 2).setDelay(this.Model.Speed / 2).setEase(LeanTweenType.easeInQuart);
        LeanTween.scale(taget, new Vector3(taget.transform.localScale.x, taget.transform.localScale.y, taget.transform.localScale.z), this.Model.Speed / 2).setDelay(this.Model.Speed / 2);
    }
    private void RotatePlayer(GameObject taget, float indexRoatation)
    {
        LeanTween.rotateY(taget, indexRoatation, this.Model.timeDelayRotate);
    }
    public void PlayerBecomeGhost(Rigidbody rb, Collider cld)
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        cld.isTrigger = true;
    }
    public void RoiXuongDayXaHoi(Vector3 index)
    {
        if(index.y < 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}
