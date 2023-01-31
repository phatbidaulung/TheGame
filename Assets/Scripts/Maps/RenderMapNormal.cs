using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ensign.Unity;

public class RenderMapNormal : Singleton<RenderMapNormal>
{
    [SerializeField] private List<GameObject> _maps;
    [SerializeField] private int _numberOfModuleMap;
    private int _numberMap;
    private float _locationNewMap;
    private void Start() 
    {
        // Create Pool Object
        foreach (var map in _maps)
        {
            map.CreatePool();
        }

        CreateMap();
    }
    private void CreateMap()
    {
        while (_numberMap < _numberOfModuleMap)
        {
            CreateNewMap();
        }
    }
    public void CreateNewMap()
    {
        if(_numberMap < _maps.Count)
        {
            float lengthMap = 10f;
            _locationNewMap += lengthMap;
            _maps[_numberMap].Spawn(new Vector3(_locationNewMap, transform.position.y, transform.position.z));
            _numberMap++;
            Debug.Log($"Number map is: {_numberMap}");
        }
    }

}
