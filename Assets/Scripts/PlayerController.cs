namespace Script
{
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Unity;
using Ensign.Tween;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animatorPlayer;
    [SerializeField] private Animation _animationPlayer;
    [SerializeField] private float _speed = 0.2f;
    private float _speedRT = 25f;
    private Vector3 _nextPosition;
    private Quaternion _nextRotation;
    public float delay = 0.1f;

    private void Start() 
    {
        _nextPosition = transform.position;
        _nextRotation = Quaternion.Euler(0f, 0f, 0f);
    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
            _animatorPlayer.SetTrigger("Jump");

        if(_animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        Debug.LogWarning($"Is play");


        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_nextPosition.x , transform.position.y, _nextPosition.z), _speed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _nextRotation, _speedRT);
        
        AddListenInput();
    }
    private void FixedUpdate() {
        
    }
    private void Movement(EMovement typeMove)
    {
        
        _animatorPlayer.SetTrigger("Jump");
        this.ActionWaitTime(delay , () => {
            
        switch (typeMove)
        {
            case EMovement.MoveToTop:
                _nextPosition += new Vector3(1f, 0f, 0f);
                _nextRotation = Quaternion.Euler(0f, 0f, 0f);
                return;
            case EMovement.MoveToBottom:
                _nextPosition -= new Vector3(1f, 0f, 0f);
                _nextRotation = Quaternion.Euler(0f, 180f, 0f);
                return;
            case EMovement.MoveToLeft:
                _nextPosition += new Vector3(0f, 0f, 1f);
                _nextRotation = Quaternion.Euler(0f, -90f, 0f);
                return;
            case EMovement.MoveToRight:
                _nextPosition -= new Vector3(0f, 0f, 1f);
                _nextRotation = Quaternion.Euler(0f, 90f, 0f);
                return;
        }
        });
    }
    private void AddListenInput()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            Movement(EMovement.MoveToTop);   
            Debug.LogWarning($"{_nextPosition}");
            return;
        }
        if(Input.GetKeyDown(KeyCode.S)){
            Movement(EMovement.MoveToBottom); 
            Debug.LogWarning($"{_nextPosition}");
            return;
        }
        if(Input.GetKeyDown(KeyCode.A)){
            Movement(EMovement.MoveToLeft); 
            Debug.LogWarning($"{_nextPosition}");
            return;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            Movement(EMovement.MoveToRight); 
            Debug.LogWarning($"{_nextPosition}");
            return;
        }
    }
}

}