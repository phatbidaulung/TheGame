using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ensign.Unity;
using Ensign.Tween;

public class EnemyController : MonoBehaviour
{
    private void OnEnable() 
    {
        // Recycle enemy when enemy don't "onTrigger" with "RecycleEnemy (gameObject)"
        this.ActionWaitTime(9f, () =>{
            if(gameObject.activeSelf == true)
                gameObject.Recycle();
        });

    }
    private void FixedUpdate() 
    {
        Move();
    }
    private void Move()
    {
        this.transform.position += transform.forward * GameManager.Instance.SpeedEnemy * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Wall")
        {
            this.gameObject.Recycle();
        }
    }
}
