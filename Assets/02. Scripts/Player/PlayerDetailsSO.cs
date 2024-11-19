using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_", menuName = "Scriptable Objects/Player/Player")]
public class PlayerDetailsSO : ScriptableObject
{
    public string characterName; // 캐릭터 이름
    public Player player; // 플레이어 프리팹
    public RuntimeAnimatorController runtimeAnimatorController;
    public Sprite playerSprite;
    public WeaponDetailsSO playerStartingWeapon; // 스타팅 무기
    [TextArea] public string characterStrength; // 캐릭터 소개를 위한 텍스트
    [TextArea] public string characterWeakness;

    [Space(10)]
    [Header("Character Stat")]
    public float Hp;
    public float HpRegen;
    //public float HpSteal;
    public int Defense;
    public int BonusDamage; 
    public int MeleeDamage;
    public int RangeDamage;
    public float Speed;
    public int Dodge;
    public float PickUpRange;
    public int ExpBonus;

}
