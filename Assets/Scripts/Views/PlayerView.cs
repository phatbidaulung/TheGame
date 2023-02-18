using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Unity.MVC;
using Ensign.Unity;
using Ensign.Tween;
public class PlayerView : View<PlayerController, PlayerModel>
{
    
    [Space, Header("Player")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _playerCollider;

    private void Update()
    {
        this.Controller.RoiXuongDayXaHoi(transform.position);
        Move();
        // MoveWithTouch();
    }
    // private void Move()
    // {
    //     if(Input.GetKeyDown(KeyCode.W))
    //     {
    //         Movement(EMovement.MoveToTop);
    //     }
    //     if(Input.GetKeyDown(KeyCode.S))
    //     {
    //         Movement(EMovement.MoveToBottom);
    //     }
    //     if(Input.GetKeyDown(KeyCode.D))
    //     {
    //         Movement(EMovement.MoveToRight);
    //     }
    //     if(Input.GetKeyDown(KeyCode.A))
    //     {
    //         Movement(EMovement.MoveToLeft);
    //     }
    // }
    
    

    private void Move()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            this.Model.startTouchPosition = Input.GetTouch(0).position;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            this.Model.currentPosition = Input.GetTouch(0).position;
            Vector2 distance = this.Model.currentPosition - this.Model.startTouchPosition;

            if(!this.Model.stopTouch)
            {
                // Left
                if(distance.x < -this.Model.swipeRange)
                {
                    this.Model.stopTouch = true;
                    this.Controller.Movement(gameObject, EMovement.MoveToLeft);
                }
                // Right
                else if(distance.x > this.Model.swipeRange)
                {
                    this.Model.stopTouch = true;
                    this.Controller.Movement(gameObject, EMovement.MoveToRight);
                }
                // Up
                else if(distance.y > this.Model.swipeRange)
                {
                    this.Model.stopTouch = true;
                    this.Controller.Movement(gameObject, EMovement.MoveToTop);
                }
                //Down
                else if(distance.y < -this.Model.swipeRange)
                {
                    this.Model.stopTouch = true;
                    this.Controller.Movement(gameObject, EMovement.MoveToBottom);
                }

            }
        }

        // Tap
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            this.Model.stopTouch = false;
            this.Model.endTouchPosition = Input.GetTouch(0).position;
            Vector2 distance = this.Model.endTouchPosition - this.Model.startTouchPosition;
            if(Mathf.Abs(distance.x) < this.Model.tapRange && Mathf.Abs(distance.y) < this.Model.tapRange)
            {
                this.Controller.Movement(gameObject, EMovement.MoveToTop);
            }
        }
    }
    public void Scale()
    {
        float valueScaleChange = 1.7f;
        float valueScaleDefaut = 2f;
        float timeChange = 0.1f;
        LeanTween.scaleY(this.gameObject,valueScaleChange, timeChange);
        LeanTween.scaleY(this.gameObject, valueScaleDefaut, timeChange).setDelay(timeChange);
    }
    
    private void MoveWithTouch()
    {
        this.Model.SwipedRight = false;
		this.Model.SwipedLeft = false;
		this.Model.SwipedUp = false;
		this.Model.SwipedDown = false;
		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				this.Model.StartPos = new Vector2(t.position.x/(float)Screen.width, t.position.y/(float)Screen.width);
				this.Model.StartTime = Time.time;
			}
			if(t.phase == TouchPhase.Ended)
			{
				if (Time.time - this.Model.StartTime > this.Model.MAX_SWIPE_TIME) // press too long
					return;

				Vector2 endPos = new Vector2(t.position.x/(float)Screen.width, t.position.y/(float)Screen.width);
				Vector2 swipe = new Vector2(endPos.x - this.Model.StartPos.x, endPos.y - this.Model.StartPos.y);
				if (swipe.magnitude < this.Model.MIN_SWIPE_DISTANCE) // Too short swipe
					return;

				if (Mathf.Abs (swipe.x) > Mathf.Abs (swipe.y)) { // Horizontal swipe
					if (swipe.x > 0) {
						this.Model.SwipedRight = true;
                        Debug.Log("right");
                        this.Controller.Movement(gameObject, EMovement.MoveToRight);
					}
					else {
						this.Model.SwipedLeft = true;
                        Debug.Log("left");
                        this.Controller.Movement(gameObject, EMovement.MoveToLeft);
					}
				}
				else { // Vertical swipe
					if (swipe.y > 0) {
						this.Model.SwipedUp = true;
                        Debug.Log("Up");
                        this.Controller.Movement(gameObject, EMovement.MoveToTop);
					}
					else {
						this.Model.SwipedDown = true;
                        Debug.Log("Down");
                        this.Controller.Movement(gameObject, EMovement.MoveToBottom);
					}
				}
			}
		}
    }
    public void PlayerBecomeGhost()
    {
        this.Controller.PlayerBecomeGhost(_rb, _playerCollider);
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Enemy")
        {
            GameManager.Instance.GameOver();
            SoundManager.Instance.PlaySound(EActionSound.PlayerDie);
        }
        if(other.gameObject.tag == "Plane")
        {
            this.Controller.CanMove(true);  
        }
    }
    private void OnCollisionStay(Collision other) {
        if(other.gameObject.tag == "Plane")
        {
            this.Controller.CanMove(true);  
        }
    }
    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Plane")
        {
            this.Controller.CanMove(false);  
        } 
    }
}
