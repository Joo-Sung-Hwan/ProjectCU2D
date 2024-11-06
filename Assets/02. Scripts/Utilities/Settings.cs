
using UnityEngine;

public static class Settings
{
    #region GAME SETTING
    public static string regex = @"^(?=.*[A-Za-z])[A-Za-z0-9]{2,12}$"; // �г��� ��Ģ
    #endregion

    #region PLAYER SETTING
    public static int maxWeaponCount = 6; // ���� �ִ� ������ 6
    #endregion

    #region LAYERMASK SETTING
    public static LayerMask monsterLayer = LayerMask.GetMask("Monster"); // ���� ���̾�
    #endregion
}
