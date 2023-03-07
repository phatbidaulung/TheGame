using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Ensign.Unity.MVC;
using Ensign.Unity;
using Ensign.Tween;
public class PlayerView : View<PlayerController, PlayerModel>
{
    
	public LayerMask obstacles;
    
    [Space, Header("Player")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _playerCollider;

    private void Awake() 
    {
        if(SceneManager.GetActiveScene().name == "Main"){
            Destroy(_rb);    
            Destroy(_animator);
            Destroy(GetComponent<PlayerView>());
        }
    }
    private void OnEnable()
    {
        this.Model.nextPosition = transform.position;
        this.Model.nextRotation = Quaternion.Euler(0f, 0f, 0f);
    }
    private void Update()
    {
        // this.Controller.RoiXuongDayXaHoi(transform.position);
        // Move();
        MoveWithKey();

		RaycastHit hit;
		// Physics.Raycast (transform.position, this.Model.nextPosition, out hit, 1,obstacles);

        
		// if (hit.collider == null) 
        // {
            // Debug.LogWarning("No object");
        // }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(this.Model.nextPosition.x , transform.position.y, this.Model.nextPosition.z), this.Model.speedMovement);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, this.Model.nextRotation, this.Model.speedRotation);
    }

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
                    this.Controller.PlayEffect(_animator);
                    this.ActionWaitTime(this.Model.timeDelayAnimation, () => {
                        Physics.Raycast (transform.position, transform.position += new Vector3(0f, 0f, 1f), out this.Model.hit, 1,obstacles);
                        if (this.Model.hit.collider != null) 
                            this.Controller.Movement(EMovement.MoveToLeft);
                    });
                }
                // Right
                else if(distance.x > this.Model.swipeRange)
                {
                    this.Model.stopTouch = true;
                    this.Controller.PlayEffect(_animator);
                    this.ActionWaitTime(this.Model.timeDelayAnimation, () => {
                        this.Controller.Movement(EMovement.MoveToRight);
                    });
                }
                // Up
                else if(distance.y > this.Model.swipeRange)
                {
                    this.Model.stopTouch = true;
                    this.Controller.PlayEffect(_animator);
                    this.ActionWaitTime(this.Model.timeDelayAnimation, () => {
                        this.Controller.Movement(EMovement.MoveToTop);
                    });
                }
                //Down
                else if(distance.y < -this.Model.swipeRange)
                {
                    this.Model.stopTouch = true;
                    this.Controller.PlayEffect(_animator);
                    this.ActionWaitTime(this.Model.timeDelayAnimation, () => {
                        this.Controller.Movement(EMovement.MoveToBottom);
                    });
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
                this.Controller.PlayEffect(_animator);
                this.ActionWaitTime(this.Model.timeDelayAnimation, () => {
                    this.Controller.Movement(EMovement.MoveToTop);
                });
            }
        }
    } 
    private void MoveWithKey()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            this.Controller.PlayEffect(_animator);
            this.ActionWaitTime(this.Model.timeDelayAnimation, () => {
                Physics.Raycast (transform.position, new Vector3(1f, 0f, 0f), out this.Model.hit, 1,obstacles);
                if (this.Model.hit.collider == null) 
                    this.Controller.Movement(EMovement.MoveToTop);   
            });
            return;
        }
        if(Input.GetKeyDown(KeyCode.S)){
            this.Controller.PlayEffect(_animator);
            this.ActionWaitTime(this.Model.timeDelayAnimation, () => {
                Physics.Raycast (transform.position, new Vector3(-1f, 0f, 0f), out this.Model.hit, 1,obstacles);
                if (this.Model.hit.collider == null) 
                    this.Controller.Movement(EMovement.MoveToBottom); 
            });
            return;
        }
        if(Input.GetKeyDown(KeyCode.A)){
            this.Controller.PlayEffect(_animator);
            this.ActionWaitTime(this.Model.timeDelayAnimation, () => {
                Physics.Raycast (transform.position, new Vector3(0f, 0f, 1f), out this.Model.hit, 1,obstacles);
                if (this.Model.hit.collider == null) 
                    this.Controller.Movement(EMovement.MoveToLeft); 
            });
            return;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            this.Controller.PlayEffect(_animator);
            this.ActionWaitTime(this.Model.timeDelayAnimation, () => {
                Physics.Raycast (transform.position, new Vector3(0f, 0f, -1f), out this.Model.hit, 1,obstacles);
                if (this.Model.hit.collider == null) 
                    this.Controller.Movement(EMovement.MoveToRight); 
            });
            return;
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
    }
}
