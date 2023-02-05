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

    [Space, Header("Script Object")]
    [SerializeField] private UIManager _uiManager;

    private void Update()
    {
        this.Controller.RoiXuongDayXaHoi(transform.position);
        Move();
        MoveWithTouch();
    }
    private void Move()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Movement(EMovement.MoveToTop);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Movement(EMovement.MoveToBottom);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            Movement(EMovement.MoveToRight);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            Movement(EMovement.MoveToLeft);
        }
    }
    public void Movement(EMovement typeMove)
    {
        if(GameManager.Instance.StatusGameIs() != EStatusGame.GameOver && this.Model.InPlane)
        {
            switch (typeMove)
            {
                case EMovement.MoveToTop:
                    this.Model.NextPosition = transform.position + new Vector3(1, 0, 0);
                    LeanTween.moveLocalX(gameObject, this.Model.NextPosition.x, this.Model.Speed);
                    RotatePlayer(0);
                    _animator.SetTrigger("Jump");
                    GameManager.Instance.IncreaseScore();
                    break;
                case EMovement.MoveToBottom:
                    this.Model.NextPosition = transform.position - new Vector3(1, 0, 0);
                    LeanTween.moveLocalX(gameObject, this.Model.NextPosition.x, this.Model.Speed);
                    RotatePlayer(180);
                    _animator.SetTrigger("Jump");
                    _uiManager.OpenPopup(EActionUI.PopupStatusRealTime);
                    break;
                case EMovement.MoveToLeft:
                    this.Model.NextPosition = transform.position + new Vector3(0, 0, 1);
                    LeanTween.moveLocalZ(gameObject, this.Model.NextPosition.z, this.Model.Speed);
                    RotatePlayer(-90);
                    _animator.SetTrigger("Jump");
                    break;
                case EMovement.MoveToRight:
                    this.Model.NextPosition = transform.position - new Vector3(0, 0, 1);
                    LeanTween.moveLocalZ(gameObject, this.Model.NextPosition.z, this.Model.Speed);
                    RotatePlayer(90);
                    _animator.SetTrigger("Jump");
                    break;
                
            }

            this.Controller.Jump(_rb);
            this.Controller.PreventPlayerTurning(transform.position);
            SoundManager.Instance.PlaySound(EActionSound.PlayerMove);
        }
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
                        Movement(EMovement.MoveToRight);
					}
					else {
						this.Model.SwipedLeft = true;
                        Debug.Log("left");
                        Movement(EMovement.MoveToLeft);
					}
				}
				else { // Vertical swipe
					if (swipe.y > 0) {
						this.Model.SwipedUp = true;
                        Debug.Log("Up");
                        Movement(EMovement.MoveToTop);
					}
					else {
						this.Model.SwipedDown = true;
                        Debug.Log("Down");
                        Movement(EMovement.MoveToBottom);
					}
				}
			}
		}
    }
    private void RotatePlayer(float index)
    {
        float timeDelayRotate = 0.2f;
        LeanTween.rotateY(gameObject, index, timeDelayRotate);
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
