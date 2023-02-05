using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    ListModel _listModel = new ListModel();

    // Only endless mode
    public void SaveData()
    {
        this.LoadDataFromFile();
        _listModel.PlayerModel.Add(new DataModel
        {
            TypeMap = GameManager.Instance.TypeMapInThisSceneIs(),
            LevelMap = GameManager.Instance.LevelMapIs(),
            Score = GameManager.Instance.TypeMapInThisSceneIs() == ETypeMap.EndLessMap ? GameManager.Instance.Score() : 0,
            Finish = GameManager.Instance.StatusGameIs() == EStatusGame.Win ? true : false
        });

        this.SaveDataToFile();
    }
    public void SaveDataToFile()
    {
        try
        {
            //Save data to directory
            var serializer = new JsonSerializer();
            using var sw = new StreamWriter(Application.persistentDataPath + "/dataUser.json");
            using JsonWriter writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, _listModel);
        }
        catch 
        { 
            //Create empty data when there is no file
            this.CreateEmptyFile(); 
        }
    }

    public void LoadDataFromFile()
    {
        try
        {
            //Get data from directory and assign it to model
            var outputJson = File.ReadAllText(Application.persistentDataPath + "/dataUser.json");
            var loadedUserData = JsonConvert.DeserializeObject<ListModel>(outputJson);
            _listModel.PlayerModel = loadedUserData.PlayerModel;
        }
        catch
        {
            //Create empty data when there is no file
            this.CreateEmptyFile();
        }
    }

    public void CreateEmptyFile()
    {
        ListModel __listModel = new ListModel();
        var serializer = new JsonSerializer();
        using var sw = new StreamWriter(Application.persistentDataPath + "/dataUser.json");
        using JsonWriter writer = new JsonTextWriter(sw);
        serializer.Serialize(writer, __listModel);
    }
    public ListModel ListModel()
    {
        this.LoadDataFromFile();
        return _listModel;
    }
}