using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Unity.MVC;
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
        RoiXuongDayXaHoi();
        if(GameManager.Instance.StatusGameIs() != EStatusGame.GameOver){
            Move();
            }
            // MovePlayer();
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
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3(1, 0, 0);
            PreventPlayerTurning();
            RotatePlayer(0);
            Jump();
            GameManager.Instance.IncreaseScore();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            transform.position -= new Vector3(1, 0, 0);
            PreventPlayerTurning();
            RotatePlayer(180);
            Jump();
            _uiManager.OpenPopup(EActionUI.PopupStatusRealTime);
            Debug.Log("You can't comeback");
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            transform.position -= new Vector3(0, 0, 1);
            PreventPlayerTurning();
            RotatePlayer(90);
            Jump();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(0, 0, 1);
            PreventPlayerTurning();
            RotatePlayer(-90);
            Jump();
        }
    }
    private void PreventPlayerTurning()
    {
        // Deduction 2 because player can come back 2 times. with time 3rd game over
        if(transform.position.x <= (GameManager.Instance.MaxPositionPlayer().x - 2))
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
