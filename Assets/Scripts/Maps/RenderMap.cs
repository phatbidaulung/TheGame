using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ensign.Unity;

public class RenderMap : MonoBehaviour
{
    [SerializeField] private ETypeMap _typeMap;
    [SerializeField] private List<GameObject> _maps;
    [SerializeField] private List<GameObject> _mapsCreated;
    [SerializeField] private int _numberOfModuleMap;
    private int _numeberMapOfList;
    private int _numberMap;
    private float _locationNewMap;
    private void Start() 
    {
        CreatePoolMapModule();
        CreateMap();
    }
    private void CreatePoolMapModule()
    {
        foreach (var map in _maps)
        {
            map.CreatePool();
        }
    }
    private void CreateMap()
    {
        for(int i=0; i < _numberOfModuleMap; i++){
            CreateNewMap();
        }
    }
    public void CreateNewMap()
    {
        float lengthMap = 10f;
        _locationNewMap += lengthMap;

        switch (_typeMap)
        {
            case ETypeMap.NormalMap:
                if(_numberMap < _maps.Count)
                {
                    _mapsCreated.Add(_maps[_numberMap].Spawn(new Vector3(_locationNewMap, transform.position.y, transform.position.z)));
                    _numberMap++;
                }
                break;
            case ETypeMap.EndLessMap:
                _mapsCreated.Add(RandomMap().Spawn(new Vector3(_locationNewMap, transform.position.y, transform.position.z)));
                break;
        }
    }

    public void RecycleMap()
    {
        _mapsCreated[_numeberMapOfList].Recycle();
        _numeberMapOfList++;
        Debug.Log("Reee");
        Debug.Log(_numeberMapOfList);
    }
    private GameObject RandomMap()
    {
        int index = Random.Range(0, _maps.Count);
        return _maps[index];
    }
}
