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
    private float _lengthMap = 10f;
    private float _locationNewMap;
    private void Start() 
    {
        _locationNewMap += _typeMap == ETypeMap.EndLessMap ? _lengthMap : 0f;
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

        switch (_typeMap)
        {
            case ETypeMap.NormalMap:
                if(_numberMap < _maps.Count)
                {
                    _mapsCreated.Add(_maps[_numberMap].Spawn(new Vector3(_locationNewMap, _maps[_numberMap].transform.position.y, _maps[_numberMap].transform.position.z)));
                    _numberMap++;
                }
                break;
            case ETypeMap.EndLessMap:
                _mapsCreated.Add(RandomMap().Spawn(new Vector3(_locationNewMap, RandomMap().transform.position.y, RandomMap().transform.position.z)));
                break;
        }

        _locationNewMap += _lengthMap;
    }

    public void RecycleMap()
    {
        _mapsCreated[_numeberMapOfList].Recycle();
        _numeberMapOfList++;
    }
    private GameObject RandomMap()
    {
        int index = Random.Range(0, _maps.Count);
        return _maps[index];
    }
}
