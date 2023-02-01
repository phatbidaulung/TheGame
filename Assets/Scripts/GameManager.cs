using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ensign.Unity;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerView _player;
    [SerializeField] private ETypeMap _typeMap;
    [SerializeField] private ELevel _levelMap;
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
    [SerializeField] private DataManager _dataManager;
    #endregion
    private void Start() 
    {

    }
    public void IncreaseScore()
    {
        // Only endless mode
        if((_player.transform.position.x > _maxPositionPlayer.x) && (TypeMapInThisSceneIs() == ETypeMap.EndLessMap))
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
        _dataManager.SaveData();
        Debug.Log("GameOver ---- GameManager");
    }

    // Only normal maps
    public void WinGame()
    {
        _statusGame = EStatusGame.Win;
        _player.PlayerBecomeGhost();
        _uiManager.OpenPopupStatusGame();
        _dataManager.SaveData();
        Debug.Log("You ------ win");
    }
    public Vector3 MaxPositionPlayer() => _maxPositionPlayer;
    public float Score() => _score;
    public ETypeMap TypeMapInThisSceneIs() => _typeMap;
    public EStatusGame StatusGameIs() => _statusGame;
    public ELevel LevelMapIs() => _levelMap;
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