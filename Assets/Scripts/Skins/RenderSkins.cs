using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSkins : MonoBehaviour
{
    [SerializeField] private SkinDataBase _skinDB;
    private int _indexSkin;

    private void Awake() 
    {
        _indexSkin = PlayerPrefs.GetInt("selectSkin");
        Skins _skins = _skinDB.GetSkins(_indexSkin);
        Instantiate(_skins.skin);
    }
}
