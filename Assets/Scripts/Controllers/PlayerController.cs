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
    public void Jump(GameObject taget)
    {
        LeanTween.moveLocalY(taget, taget.transform.position.y + this.Model.JumpForce, this.Model.Speed / 2).setEase(LeanTweenType.easeOutQuart);
        LeanTween.moveLocalY(taget, taget.transform.position.y, this.Model.Speed / 2).setDelay(this.Model.Speed / 2).setEase(LeanTweenType.easeInQuart);
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
            // Debug.Log("You are DatVila :>");
            GameManager.Instance.GameOver();
        }
    }
}
