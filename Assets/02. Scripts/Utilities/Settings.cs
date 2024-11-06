
using UnityEngine;

public static class Settings
{
    #region GAME SETTING
    public static string regex = @"^(?=.*[A-Za-z])[A-Za-z0-9]{2,12}$"; // 닉네임 규칙
    #endregion

    #region PLAYER SETTING
    public static int maxWeaponCount = 6; // 무기 최대 보유수 6
    #endregion

    #region LAYERMASK SETTING
    public static LayerMask monsterLayer = LayerMask.GetMask("Monster"); // 몬스터 레이어
    #endregion
}
