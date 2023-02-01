using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Unity;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerView _player;
    [SerializeField] private ETypeMap _typeMap;
    private EStatusGame _statusGame;
    private float _score;
    #region Player
    private Vector3 _maxPositionPlayer;
    #endregion

    #region Enemy
    [HideInInspector] public float SpeedEnemy = 3f;
    #endregion
    #region UI
    [SerializeField] private UIManager _uiManager;
    #endregion
    private void Start() 
    {

    }
    public void IncreaseScore()
    {
        if(_player.transform.position.x > _maxPositionPlayer.x)
        {
            // Increase score
            _score++;
            _maxPositionPlayer = _player.transform.position;
            Debug.Log($"Score is: {_score}");

            // Increases enemy's speed when player's score is greater than a multiple of 10 (10, 20, 30, 40)
            if(_score % 10 == 0)
            { 
                Debug.Log($"Change speed enemy. Speed enemy is: {SpeedEnemy}");
                float increaseSpeed = 1f;
                SpeedEnemy += increaseSpeed;
            }

            // Change score in screen
            _uiManager.ChangeScore(_score.ToString());
        }
    }

    public void GameOver()
    {
        _statusGame = EStatusGame.GameOver;
        _player.PlayerBecomeGhost();
        _uiManager.OpenPopupStatusGame();
        Debug.Log("GameOver ---- GameManager");
    }

    public void WinGame()
    {
        _statusGame = EStatusGame.Win;
        _player.PlayerBecomeGhost();
        _uiManager.OpenPopupStatusGame();
        Debug.Log("You ------ win");
    }
    public Vector3 MaxPositionPlayer() => _maxPositionPlayer;
    public float Score() => _score;
    public ETypeMap TypeMapInThisSceneIs() => _typeMap;
    public EStatusGame StatusGameIs() => _statusGame;
}
public enum ETypeMap
{
    NormalMap,
    EndLessMap
}
public enum EStatusGame
{
    Playing,
    GameOver,
    Win
}
public enum ELevel
{
    Level01,
    Level02,
    Level03
}