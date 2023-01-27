using Ensign;
using Ensign.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RenderMap : Singleton<RenderMap>
{
    [SerializeField] private GameObject _map1;
    [SerializeField] private GameObject _map2;
    [SerializeField] private GameObject _map3;

    private float _locationMap;
    private void Awake() {
        _map1.CreatePool();
        _map2.CreatePool();
        _map3.CreatePool();

        CreateMap();
    }
    public void CreateNewMap()
    {
        float lengthMap = 10f;
        _locationMap += lengthMap;
        RandomMap().Spawn(new Vector3(_locationMap, transform.position.y, transform.position.z));
    }
    private void CreateMap()
    {
        for(int i=0; i < 3; i++){
            float lengthMap = 10f;
            _locationMap += lengthMap;
            RandomMap().Spawn(new Vector3(_locationMap, transform.position.y, transform.position.z));
        }
    }

    private GameObject RandomMap()
    {
        int index = Random.Range(1, 3);
        switch (index)
        {
            case 1:
            Debug.Log("1");
                return _map1;
            case 2:
            Debug.Log("2");
                return _map2;
            case 3:
            Debug.Log("3");
                return _map3;
                
        }
        return null;
    }
}
