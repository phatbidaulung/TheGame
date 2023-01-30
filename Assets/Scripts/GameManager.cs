using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Unity;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private ETypeMap _typeMap;
    public ETypeMap TypeMapInThisSceneIs() => _typeMap;
}
public enum ETypeMap
{
    NormalMap,
    EndLessMap
}