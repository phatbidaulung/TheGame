using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Test : MonoBehaviour 
{
	[SerializeField] private GameObject _player;
    [SerializeField] private NavMeshAgent _enemy;

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate() {
        Move();
    }
    private void Move()
    {
        _enemy.SetDestination(_player.transform.position);       
    }
}