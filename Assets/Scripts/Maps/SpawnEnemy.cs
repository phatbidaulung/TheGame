using UnityEngine;
using Ensign.Unity;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _timeInstantiate;
    private float _timeRandom;

    private void Awake() 
    {
        _enemy.CreatePool();
    }
    private void OnEnable() 
    {
        SpawnGameobjet();
        _timeRandom = Random.Range(0f, 1f);
    }
    private void SpawnGameobjet()
    {
        if(GameManager.Instance.StatusGameIs() == EStatusGame.Playing)
        {
            this.ActionWaitTime(_timeInstantiate + _timeRandom, () => {
                _enemy.Spawn(transform.position, transform.rotation);
                _timeRandom = Random.Range(0f, 0.5f);
                SpawnGameobjet();
            });
        }
    }
}
