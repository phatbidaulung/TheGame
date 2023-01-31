using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ensign.Unity;

public class EnemyController : MonoBehaviour
{
    private void OnEnable() 
    {
        // Recycle enemy when enemy don't "onTrigger" with "RecycleEnemy (gameObject)"
        this.ActionWaitTime(15f, () =>{
            if(gameObject.activeSelf == true)
                gameObject.Recycle();
        });

    }
    private void FixedUpdate() 
    {
        Move();
        if(transform.position.z >= 20)
            transform.position = new Vector3(transform.position.x, transform.position.y, -15);
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
