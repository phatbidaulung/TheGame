using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Unity.MVC;
public class PlayerController : Controller<PlayerController, PlayerModel>
{
    protected override void Init()
    {
        base.Init();
        this.ChangeModel(new PlayerModel
        {
            JumpForce = 100f,
            TimeDelay = 0.1f,
            Speed = 0.5f,
            InPlane = true
        });
    }
    public Vector3 ChangeNextPosition(Vector3 input) => this.Model.NextPosition = input;
    public Vector3 ChangeCurrentPosition(Vector3 input) => this.Model.CurrentPosition = input;
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
    public void Jump(Rigidbody rb)
    {
        rb.AddForce(0f, this.Model.JumpForce, 0f);
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
