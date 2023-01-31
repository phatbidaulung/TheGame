using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3(1, 0, 0);
            GameManager.Instance.IncreaseScore();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            transform.position -= new Vector3(1, 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            transform.position -= new Vector3(0, 0, 1);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(0, 0, 1);
        }
    }
}
