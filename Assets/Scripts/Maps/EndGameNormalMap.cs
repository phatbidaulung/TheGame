using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameNormalMap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            Debug.Log("You win");
        }
    }
}
