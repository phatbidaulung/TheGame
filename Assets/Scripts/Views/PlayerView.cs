using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Unity.MVC;
public class PlayerView : View<PlayerController, PlayerModel>
{
    [SerializeField] private Rigidbody _rb;
    
    private void Start()
    {
        this.Controller.ChangeCurrentPosition(transform.position);
    }

    private void Update()
    {
        if(this.Model.InPlane)
            MovePlayer();
    }
    private void MovePlayer()
    { 
        if(transform.position != new Vector3(this.Model.CurrentPosition.x, transform.position.y, this.Model.CurrentPosition.z) + this.Model.NextPosition)
        {
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
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                this.Controller.ChangeNextPosition(new Vector3(-1, this.Model.NextPosition.y, this.Model.NextPosition.z));
                RotatePlayer(180);
                Jump();
            }
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
    private void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.tag == "Plane"){
            this.Controller.CanMove(true);  
        }
    }
    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Plane"){
            this.Controller.CanMove(false);  
        } 
    }
}
