using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Unity.MVC;
using Ensign.Unity;
public class PlayerView : View<PlayerController, PlayerModel>
{
    
    [Space, Header("Player")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _playerCollider;

    [Space, Header("Script Object")]
    [SerializeField] private UIManager _uiManager;

    private float _timeDelay = 0.1f;

    private void Start()
    {
        this.Controller.ChangeCurrentPosition(transform.position);
    }

    private void Update()
    {
        RoiXuongDayXaHoi();
        Move();
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
                Jump();
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                this.Controller.ChangeNextPosition(new Vector3(this.Model.NextPosition.x, this.Model.NextPosition.y, -1));
                RotatePlayer(90);
                Jump();
            }
            else if(Input.GetKeyDown(KeyCode.W))
            {
                this.Controller.ChangeNextPosition(new Vector3(1, this.Model.NextPosition.y, this.Model.NextPosition.z));
                RotatePlayer(0);
                Jump();
                GameManager.Instance.IncreaseScore();
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                this.Controller.ChangeNextPosition(new Vector3(-1, this.Model.NextPosition.y, this.Model.NextPosition.z));
                RotatePlayer(180);
                Jump();
            }
        }
    }
    
    // Test move player
    void Move()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetMouseButtonDown(0))
        {
            MoveToTop();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            MoveToBottom();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            MoveToRight();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            MoveToLeft();
        }
    }
    public void MoveToTop()
    {
        if(GameManager.Instance.StatusGameIs() != EStatusGame.GameOver && this.Model.InPlane)
        {
            Jump();
            this.ActionWaitTime(_timeDelay, () => {
                transform.position += new Vector3(1, 0, 0);
            });
            PreventPlayerTurning();
            RotatePlayer(0);
            GameManager.Instance.IncreaseScore();
            SoundManager.Instance.PlaySound(EActionSound.PlayerMove);
        }
    }
    public void MoveToBottom()
    {
        if(GameManager.Instance.StatusGameIs() != EStatusGame.GameOver && this.Model.InPlane)
        {
            Jump();
            this.ActionWaitTime(_timeDelay, () => {
                transform.position -= new Vector3(1, 0, 0);
            });
            PreventPlayerTurning();
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
            Jump();
            this.ActionWaitTime(_timeDelay, () => {
                transform.position += new Vector3(0, 0, 1);
            });
            PreventPlayerTurning();
            RotatePlayer(-90);
            SoundManager.Instance.PlaySound(EActionSound.PlayerMove);
        }
    }
    public void MoveToRight()
    {
        if(GameManager.Instance.StatusGameIs() != EStatusGame.GameOver && this.Model.InPlane)
        {
            Jump();
            this.ActionWaitTime(_timeDelay, () => {
                transform.position -= new Vector3(0, 0, 1);
            });
            PreventPlayerTurning();
            RotatePlayer(90);
            SoundManager.Instance.PlaySound(EActionSound.PlayerMove);
        }
    }
    private void PreventPlayerTurning()
    {
        // Deduction 2 because player can come back 3 times. with time 3rd game over
        if(transform.position.x <= (GameManager.Instance.MaxPositionPlayer().x - 3))
        {
            GameManager.Instance.GameOver();
        }
    }
    private void RotatePlayer(float index)
    {
        transform.eulerAngles  = new Vector3(0, index, 0);
    }
    private void Jump()
    {
        _rb.AddForce(0f, this.Model.JumpForce, 0f);
    }
    public void PlayerBecomeGhost()
    {
        _rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        _playerCollider.isTrigger = true;
    }
    private void RoiXuongDayXaHoi()
    {
        if(transform.position.y < 0)
        {
            Debug.Log("You are DatVila :>");
            GameManager.Instance.GameOver();
        }
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
