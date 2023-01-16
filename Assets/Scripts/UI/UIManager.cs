using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] protected GameObject _popupSetting;
    protected void OpenPopup(GameObject taget)
    {
        taget.SetActive(true);
    }
    protected void ClosePopup(GameObject taget)
    {
        taget.SetActive(false);
    }
}
