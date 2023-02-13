using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour 
{
	public GameObject _loandins;
	public Slider _sliderLoading;
	public void LoadScene(string nameScene)
    {
        StartCoroutine(LoadSceneAsync("EndlessMode"));
    }
    IEnumerator LoadSceneAsync(string nameScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);
        _loandins.SetActive(true);
        
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f) / 10f;
            _sliderLoading.value = progressValue;
            yield return null;
        }
    }
}