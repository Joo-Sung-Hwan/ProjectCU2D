using UnityEngine;



public class WeaponLevelUpData : ScriptableObject, ILevelUpData
{
    public int id;
    public string description;
    public EWeaponStatType statType;  // enum
    public int value;
    public ELevelUpGrade ratio;

    public int GetRatio()
        => (int)ratio;

    public int GetStatType()
    => (int)statType;
}
