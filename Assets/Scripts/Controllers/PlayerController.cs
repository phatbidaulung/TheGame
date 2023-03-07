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
            // Value movement
            nextPosition        = default,
            nextRotation        = default,
            speedMovement       = 0.2f,
            speedRotation       = 20f,
            timeDelayAnimation  = 0.15f,

            // Value touch screen
            startTouchPosition = default,
            currentPosition = default,
            endTouchPosition = default,
            stopTouch = false,
            swipeRange = 50f,
            tapRange = 10
        });
    }
    public void PreventPlayerTurning(Vector3 index)
    {
        // Deduction 2 because player can come back 3 times. with time 3rd game over
        if(index.x <= (GameManager.Instance.MaxPositionPlayer().x - 3))
        {
            GameManager.Instance.GameOver();
        }
    }
    public void Movement(EMovement typeMove)
    {
        if(GameManager.Instance.StatusGameIs() != EStatusGame.GameOver)
        {
            switch (typeMove)
            {
                case EMovement.MoveToTop:
                    this.Model.nextPosition += new Vector3(1f, 0f, 0f);
                    this.Model.nextRotation = Quaternion.Euler(0f, 0f, 0f);
                    GameManager.Instance.IncreaseScore();
                    return;
                case EMovement.MoveToBottom:
                    this.Model.nextPosition -= new Vector3(1f, 0f, 0f);
                    this.Model.nextRotation = Quaternion.Euler(0f, 180f, 0f);
                    return;
                case EMovement.MoveToLeft:
                    this.Model.nextPosition += new Vector3(0f, 0f, 1f);
                    this.Model.nextRotation = Quaternion.Euler(0f, -90f, 0f);
                    return;
                case EMovement.MoveToRight:
                    this.Model.nextPosition -= new Vector3(0f, 0f, 1f);
                    this.Model.nextRotation = Quaternion.Euler(0f, 90f, 0f);
                    return;
            }
        }
    }
    public void PlayEffect(Animator animator)
    {
        animator.SetTrigger("Jump");
        SoundManager.Instance.PlaySound(EActionSound.PlayerMove);
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
