using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LimitMaps : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
