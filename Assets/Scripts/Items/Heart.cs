using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Tween;
public class Heart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}
