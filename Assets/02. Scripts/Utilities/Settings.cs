
using UnityEngine;

public static class Settings
{
    #region GAME SETTING
    public static string regex = @"^(?=.*[A-Za-z])[A-Za-z0-9]{2,12}$"; // 닉네임 규칙
    public static int spawnInterval = 22233; // 스폰간격 1초
    public static int waveTimer = 60; // 웨이브 지속시간 60초
    public static int stageBoundary = 15; // 스테이지 +- 크기 (정사각형이므로 -15 ~ 15)
    #endregion


    #region PLAYER SETTING
    public static int maxWeaponCount = 6; // 무기 최대 보유수 6
    #endregion


    #region ANIMATOR PARAMETER
    
    #endregion


    #region LAYERMASK SETTING
    public static LayerMask monsterLayer = LayerMask.GetMask("Monster"); // 몬스터 레이어
    #endregion
}
