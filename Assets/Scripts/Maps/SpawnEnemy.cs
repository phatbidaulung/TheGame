using UnityEngine;
using Ensign.Unity;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private EMovement _typeMovement;
    [SerializeField] private NewEnemyController _enemyController;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _timeInstantiate;
    private float _timeRandom;

    private void Awake() 
    {
        _enemy.CreatePool();
        _enemyController.typeMove = _typeMovement;
    }
    private void OnEnable() 
    {
        _timeRandom = Random.Range(0f, 1f);
        InvokeRepeating("SpawnGameobjet", 1f, _timeInstantiate + _timeRandom);
    }
    private void SpawnGameobjet()
    {
        if(GameManager.Instance.StatusGameIs() == EStatusGame.Playing)
        {
            _enemy.Spawn(transform.position, transform.rotation);
        }
    }
}
