using Ensign;
using Ensign.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RenderMapEndless : Singleton<RenderMapEndless>
{
    [SerializeField] private List<GameObject> _maps;
    [SerializeField] private int _numberOfModule;

    private float _locationMap;
    private void Awake() 
    {
        CreateMap();
        CreatePoolMapModule();
    }

    private void CreatePoolMapModule()
    {
        foreach (var map in _maps)
        {
            map.CreatePool();
        }
    }
    public void CreateNewMap()
    {
        float lengthMap = 10f;
        _locationMap += lengthMap;
        RandomMap().Spawn(new Vector3(_locationMap, transform.position.y, transform.position.z));
    }
    private void CreateMap()
    {
        for(int i=0; i < _numberOfModule; i++){
            CreateNewMap();
        }
    }

    private GameObject RandomMap()
    {
        int index = Random.Range(0, _maps.Count);
        Debug.Log($"Number map is: {index}");
        return _maps[index];
    }
}
