using UnityEngine;

using Ensign.Unity;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerView _player;
    [SerializeField] private RenderMap _renderMap;
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
    private void Start() {
        if(_player == null)
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerView>();
    }
    public void IncreaseScore()
    {
        if((_player.transform.position.x > _maxPositionPlayer.x))
        {
            // Increase score
            _score++;
            _maxPositionPlayer = _player.transform.position;

            // Increases enemy's speed when player's score is greater than a multiple of 10 (10, 20, 30, 40)
            if(_score % 10 == 0)
            { 
                float increaseSpeed = 1f;
                SpeedEnemy += increaseSpeed;
            }

            // Change score in screen (only endless mode)
            if(_typeMap == ETypeMap.EndLessMap)
                _uiManager.ChangeScore(_score.ToString());
            RenderMap();
        }
    }

    public void RenderMap()
    {
        if(_score >= 20 && _score % 10 == 0)
        {        
            _renderMap.RecycleMap();
            _renderMap.CreateNewMap();
            Debug.Log("hhh");
        }
    }

    public void GameOver()
    {
        if(_statusGame != EStatusGame.GameOver)
        {
            _statusGame = EStatusGame.GameOver;
            _player.PlayerBecomeGhost();
            _uiManager.OpenPopupStatusGame();
            _dataManager.SaveData();
            SoundManager.Instance.PlaySound(EActionSound.GameOver);
            Debug.Log("GameOver ---- GameManager");
        }
    }

    ///<summary>
    /// Only normal maps
    ///</summary>
    public void WinGame()
    {
        if(_statusGame != EStatusGame.Win)
        {
            _statusGame = EStatusGame.Win;
            _player.PlayerBecomeGhost();
            _uiManager.OpenPopupStatusGame();
            _dataManager.SaveData();
            SoundManager.Instance.PlaySound(EActionSound.WinGame);
            Debug.Log("You ------ win");
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
    }

    private void CreateNewMap()
    {
        if(_score % 10 == 0)
        {
            _renderMap.CreateNewMap();
            Debug.Log("Create new maps");
        }
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
    Lock,
    Level01,
    Level02,
    Level03,
    Level04,
    Level05,
    Level06,
    Level07,
    Level08,
    Level09,
    Level10,
    Level11,
    Level12
}