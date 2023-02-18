using UnityEngine;

using Ensign;
using Ensign.Unity.MVC;
public class PlayerModel : IDataModel
{
    public float timeDelayRotate;
    public float JumpForce {get; set;}
    public float TimeDelay {get; set;}
    public float LimitMapLeft {get; set;}
    public float LimitMapRight {get; set;}
    public float Speed {get; set;}
    public Vector3 NextPosition {get; set;} 
    public bool InPlane {get; set;}

    // value for move player with touch
    
	public float MAX_SWIPE_TIME = 0.5f; 
	
	// Factor of the screen width that we consider a swipe
	// 0.17 works well for portrait mode 16:9 phone
	public float MIN_SWIPE_DISTANCE = 0.17f;
	public bool SwipedRight = false;
	public bool SwipedLeft = false;
	public bool SwipedUp = false;
	public bool SwipedDown = false;
	public Vector2 StartPos;
	public float StartTime;

    //
    public Vector2 startTouchPosition;
    public Vector2 currentPosition;
    public Vector2 endTouchPosition;
    public bool stopTouch;
    public float swipeRange;
    public float tapRange;
}

public enum EMovement
{
    MoveToTop,
    MoveToBottom,
    MoveToLeft,
    MoveToRight
}