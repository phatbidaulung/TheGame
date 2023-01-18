using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign;
using Ensign.Unity.MVC;
public class PlayerController : Controller<PlayerController, PlayerModel>
{
    protected override void Init()
    {
        base.Init();
        this.ChangeModel(new PlayerModel
        {
            JumpForce = 100f,
            Speed = 10f,
            InPlane = true
        });
    }
    public Vector3 ChangeNextPosition(Vector3 input) => this.Model.NextPosition = input;
    public Vector3 ChangeCurrentPosition(Vector3 input) => this.Model.CurrentPosition = input;
    public void CanMove(bool input)
    {
        this.Model.InPlane = input;
    }
}
