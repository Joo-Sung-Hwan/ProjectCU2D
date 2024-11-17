using UnityEngine;



public class PlayerLevelUpData : ScriptableObject
{
    public int id;
    public string description;
    public EStatType EStatType;  // enum
    public int value;
    public int ratio;
}

public enum EStatType
{
    MaxHp,
    Damage,
}
