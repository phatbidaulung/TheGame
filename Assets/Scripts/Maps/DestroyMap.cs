using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ensign.Unity;

public class DestroyMap : MonoBehaviour
{
    [SerializeField] private GameObject _map;
    [SerializeField] private bool _createMap;
    private ETypeMap _typeMap;
    private void Awake() 
    {
        _typeMap = GameManager.Instance.TypeMapInThisSceneIs();
    } 
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            if(_createMap)
            {
                switch (_typeMap)
                {
                    case ETypeMap.EndLessMap:
                        // RenderMapEndless.Instance.CreateNewMap();
                        break;
                    case ETypeMap.NormalMap:
                        // RenderMapNormal.Instance.CreateNewMap();
                        break;
                }
                Debug.Log("Create new maps");
            }
            // RenderMapEndless.Instance.RecycleMap();
        }
    }
}