using UnityEngine;

[CreateAssetMenu(fileName = "SkinDataBase", menuName = "TheGame/SkinDataBase", order = 0)]
public class SkinDataBase : ScriptableObject 
{
    public Skins[] skins;
    public int SkinsCount
    {
        get
        {
            return skins.Length;
        }
    }
    public Skins GetSkins(int index)
    {
        return skins[index];
    }
}