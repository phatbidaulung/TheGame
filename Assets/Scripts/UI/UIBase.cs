using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Ensign;
using Ensign.Model;
using Ensign.Tween;
using Ensign.UniRx;
using Ensign.Unity;

[Resource("UIBase", IsDontDestroy = false)]
public class UIBase : MonoBehaviour
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
    protected void MoveBackgroundTo(GameObject taget, Vector3 vtcTo, float time)
    {
        LeanTween.moveLocal(taget, vtcTo, time);
    }
    protected void ChangeAlpha(CanvasGroup canvasGr, float obasity, float time)
    {
        LeanTween.alphaCanvas(canvasGr, obasity, time);
    }
    protected void ChangeScale(GameObject taget, Vector3 vct3, float time)
    {
        LeanTween.scale(taget, vct3, time);
    }
}
