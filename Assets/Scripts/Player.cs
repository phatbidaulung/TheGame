using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected bool inPlane;	
    private void OnCollisionEnter(Collision other) 
    {
		if(other.gameObject.tag == "Plane")
        {
			inPlane = true;
            Debug.LogWarning("Chamm");
		}
	}
	private void OnCollisionExit(Collision other) 
    {
		if(other.gameObject.tag == "Plane"){
			inPlane = false;
		}
	}
}
