using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ensign.Unity;

public class DestroyMap : MonoBehaviour
{
    [SerializeField] private GameObject _map;
    [SerializeField] private bool _createMap;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            if(_createMap)
                RenderMap.Instance.CreateNewMap();
            _map.Recycle();
            Debug.Log("Create new maps");
        }
    }
}
