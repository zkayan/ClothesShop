using UnityEngine;

[CreateAssetMenu(fileName = "New Clothes list", menuName = "Clothes")]
public class SO_Clothes : ScriptableObject
{
    public Clothes[] clothesTypes;
}

[System.Serializable]
public class Clothes
{
    public string bodyPartType;
    public SO_Cloth[] clothes;
}
