using Ensign.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RenderMapEndles : MonoBehaviour
{
    [SerializeField] private List<GameObject> _maps;
    [SerializeField] private List<GameObject> _mapsCreated;
    [SerializeField] private int _numberOfModule;
    private int _numeberMapOfList;

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
        _mapsCreated.Add(RandomMap().Spawn(new Vector3(_locationMap, transform.position.y, transform.position.z)));
    }
    private void CreateMap()
    {
        for(int i=0; i < _numberOfModule; i++){
            CreateNewMap();
        }
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
