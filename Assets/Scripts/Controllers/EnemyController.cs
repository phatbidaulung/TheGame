using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private void FixedUpdate() 
    {
        Move();
    }
    private void Move()
    {
        this.transform.position += transform.forward * _speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Wall")
        {
            this.gameObject.SetActive(false);
        }
    }
}
