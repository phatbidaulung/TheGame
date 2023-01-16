using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Ensign;
using Ensign.Model;
using Ensign.Tween;
using Ensign.UniRx;
using Ensign.Unity;

[Resource("UIManager", IsDontDestroy = false)]
public class UIManager : MonoBehaviour
{

    protected void OpenPopup(GameObject taget)
    {
        taget.SetActive(true);
    }
    protected void ClosePopup(GameObject taget)
    {
        taget.SetActive(false);
    }
    protected void LoadScenes(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
