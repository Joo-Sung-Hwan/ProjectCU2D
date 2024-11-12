
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


    #region MONSTER SETTING
    public static int monsterFireRate = 2000; // 몬스터 공격간격 (밀리세컨드)
    public static float monsterProjectileDist = 20f; // 몬스터 투사체 사거리
    #endregion


    #region ANIMATOR PARAMETER

    #endregion


    #region LAYERMASK SETTING
    public static LayerMask monsterLayer = LayerMask.GetMask("Monster"); // 몬스터 레이어
    #endregion

    #region COLOR SETTING
    public static Color32 green = new Color32(22, 135, 24, 255);
    public static Color32 beige = new Color32(207, 182, 151, 255);
    public static Color32 rare = new Color32(11, 110, 204, 255); // 파랑
    public static Color32 unique = new Color32(155, 61, 217, 255); // 보라
    public static Color32 legend = new Color32(255, 112, 120, 255); // 빨강
    public static Color32 critical = new Color32(255, 102, 2, 255); // 주황
    #endregion
}
