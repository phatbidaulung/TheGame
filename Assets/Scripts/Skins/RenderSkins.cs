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
        Instantiate(_skins.skin, new Vector3(0f, 2f, 0f), Quaternion.Euler(0f, 0f, 0f)); 
    }
}
