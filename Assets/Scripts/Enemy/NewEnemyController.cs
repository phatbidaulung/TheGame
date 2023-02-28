using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Unity;
using Ensign.Tween;
public class NewEnemyController : MonoBehaviour
{
    public EMovement typeMove;
    [SerializeField] private float _speed;
    private float _jumpForce = 0.5f;
    private Vector3 _nextPosition;
    private void Start() 
    {
        InvokeRepeating("Movement", 1f, 1f);
        this.ActionWaitTime(20f, () =>{
            if(gameObject.activeSelf == true)
                gameObject.Recycle();
        });
    }
    private void Movement()
    {
        switch(typeMove)
        {
            case EMovement.MoveToLeft:
                _nextPosition = this.transform.position + new Vector3(0f, 0f, 01);
                LeanTween.moveLocalZ(gameObject, _nextPosition.z, _speed);
                break;
            case EMovement.MoveToRight:
                _nextPosition = this.transform.position - new Vector3(0f, 0f, 1f);
                LeanTween.moveLocalZ(gameObject, _nextPosition.z, _speed);
                break;
        }
        LeanTween.moveLocalY(gameObject, this.transform.position.y + _jumpForce, _speed / 2).setEase(LeanTweenType.easeOutQuint);
        LeanTween.moveLocalY(gameObject, this.transform.position.y, _speed / 2).setDelay(_speed / 2).setEase(LeanTweenType.easeInExpo);;
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Wall")
        {
            this.gameObject.Recycle();
        }
    }
}
