using UnityEngine;

using Ensign;
using Ensign.Unity.MVC;
public class PlayerModel : IDataModel
{
    public float JumpForce {get; set;}
    public float TimeDelay {get; set;}
    public Vector3 NextPosition {get; set;} 
    public Vector3 CurrentPosition {get; set;}
    public Vector2 StartTouchPosition {get; set;}
    public Vector2 EndTouchPosition {get; set;}
    public float Speed {get; set;}
    public bool InPlane {get; set;}
}
