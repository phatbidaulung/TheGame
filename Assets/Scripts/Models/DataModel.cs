using System.Collections.Generic;

public class DataModel
{
    public ETypeMap TypeMap;
    public ELevel LevelMap;
    public float Score;
    public bool Finish;
}

public class ListModel
{
    public List<DataModel> PlayerModel = new List<DataModel>();
}