using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private void Awake() {
        // transform.position = new Vector3(transform.position.x, transform.position.y, -15);
    }
    private void FixedUpdate() 
    {
        Move();
        if(transform.position.z >= 20)
            transform.position = new Vector3(transform.position.x, transform.position.y, -15);
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
