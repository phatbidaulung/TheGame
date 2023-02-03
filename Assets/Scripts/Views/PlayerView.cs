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

    private void Start()
    {
        this.Controller.ChangeCurrentPosition(transform.position);
    }

    private void Update()
    {
        this.Controller.RoiXuongDayXaHoi(transform.position);
        Move();
        // MoveWithTouch();
    }
    private void MovePlayer()
    { 
        if(transform.position != new Vector3(this.Model.CurrentPosition.x, transform.position.y, this.Model.CurrentPosition.z) + this.Model.NextPosition)
        {
            _animator.SetTrigger("Jump");
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(this.Model.CurrentPosition.x, transform.position.y, this.Model.CurrentPosition.z) + this.Model.NextPosition, this.Model.Speed * Time.deltaTime);
        }
        else
        {
            this.Controller.ChangeNextPosition(Vector3.zero);
            this.Controller.ChangeCurrentPosition(transform.position);
            
            if(Input.GetKeyDown(KeyCode.A))
            {
                this.Controller.ChangeNextPosition(new Vector3(this.Model.NextPosition.x, this.Model.NextPosition.y, 1));
                RotatePlayer(-90);
                this.Controller.Jump(_rb);
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                this.Controller.ChangeNextPosition(new Vector3(this.Model.NextPosition.x, this.Model.NextPosition.y, -1));
                RotatePlayer(90);
                this.Controller.Jump(_rb);
            }
            else if(Input.GetKeyDown(KeyCode.W))
            {
                this.Controller.ChangeNextPosition(new Vector3(1, this.Model.NextPosition.y, this.Model.NextPosition.z));
                RotatePlayer(0);
                this.Controller.Jump(_rb);
                GameManager.Instance.IncreaseScore();
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                this.Controller.ChangeNextPosition(new Vector3(-1, this.Model.NextPosition.y, this.Model.NextPosition.z));
                RotatePlayer(180);
                this.Controller.Jump(_rb);
            }
        }
    }
    
    // Test move player
    void Move()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            MoveToTop();
            Debug.Log("W");
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            MoveToBottom();
            Debug.Log("S");
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            MoveToRight();
            Debug.Log("D");
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            MoveToLeft();
            Debug.Log("A");
        }
    }
    public void MoveToTop()
    {
        if(GameManager.Instance.StatusGameIs() != EStatusGame.GameOver && this.Model.InPlane)
        {
            this.Controller.Jump(_rb);
            this.Model.NextPosition = transform.position + new Vector3(1, 0, 0);
            LeanTween.moveLocalX(gameObject, this.Model.NextPosition.x, this.Model.Speed);
            this.Model.InPlane = false;
            this.Controller.PreventPlayerTurning(transform.position);
            RotatePlayer(0);
            GameManager.Instance.IncreaseScore();
            SoundManager.Instance.PlaySound(EActionSound.PlayerMove);
        }
    }
    public void MoveToBottom()
    {
        if(GameManager.Instance.StatusGameIs() != EStatusGame.GameOver && this.Model.InPlane)
        {
            this.Controller.Jump(_rb);
            this.Model.NextPosition = transform.position - new Vector3(1, 0, 0);
            LeanTween.moveLocalX(gameObject, this.Model.NextPosition.x, this.Model.Speed);
            this.Model.InPlane = false;
            this.Controller.PreventPlayerTurning(transform.position);
            RotatePlayer(180);
            _uiManager.OpenPopup(EActionUI.PopupStatusRealTime);
            SoundManager.Instance.PlaySound(EActionSound.PlayerMove);
            Debug.Log("You can't comeback");
        }
    }
    public void MoveToLeft()
    {
        if(GameManager.Instance.StatusGameIs() != EStatusGame.GameOver && this.Model.InPlane)
        {
            this.Controller.Jump(_rb);
            this.Model.NextPosition = transform.position + new Vector3(0, 0, 1);
            LeanTween.moveLocalZ(gameObject, this.Model.NextPosition.z, this.Model.Speed);
            this.Model.InPlane = false;
            this.Controller.PreventPlayerTurning(transform.position);
            RotatePlayer(-90);
            SoundManager.Instance.PlaySound(EActionSound.PlayerMove);
            Debug.Log(this.Model.NextPosition.z);
        }
    }
    public void MoveToRight()
    {
        if(GameManager.Instance.StatusGameIs() != EStatusGame.GameOver && this.Model.InPlane)
        {
            this.Controller.Jump(_rb);
            this.Model.NextPosition = transform.position - new Vector3(0, 0, 1);
            LeanTween.moveLocalZ(gameObject, this.Model.NextPosition.z, this.Model.Speed);
            this.Model.InPlane = false;
            this.Controller.PreventPlayerTurning(transform.position);
            RotatePlayer(90);
            SoundManager.Instance.PlaySound(EActionSound.PlayerMove);
        }
    }
    
    private void MoveWithTouch()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            this.Model.StartTouchPosition = Input.GetTouch(0).position;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            this.Model.EndTouchPosition = Input.GetTouch(0).position;

            if(this.Model.EndTouchPosition.x < this.Model.StartTouchPosition.x)
            {
                MoveToRight();
            }

            if(this.Model.EndTouchPosition.x > this.Model.StartTouchPosition.x)
            {
                MoveToLeft();
            }
        }
    }
    private void RotatePlayer(float index)
    {
        transform.rotation = Quaternion.Euler(0f, index, 0f);
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
    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Plane")
        {
            this.Controller.CanMove(false);  
        } 
    }
}
