using UnityEngine;

using Ensign;
public class PlayerModel : IDataModel
{
    // Value movement
    public float speedMovement;
    public float speedRotation;
    public float timeDelayAnimation;
    public Vector3 nextPosition;
    public Quaternion nextRotation;
    public RaycastHit hit; 	
    public LayerMask obstacles;

    // Value touch screen
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