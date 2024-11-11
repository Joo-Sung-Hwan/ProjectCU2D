
using UnityEngine;

public static class Settings
{
    #region GAME SETTING
    public static string regex = @"^(?=.*[A-Za-z])[A-Za-z0-9]{2,12}$"; // �г��� ��Ģ
    public static int spawnInterval = 22233; // �������� 1��
    public static int waveTimer = 60; // ���̺� ���ӽð� 60��
    public static int stageBoundary = 15; // �������� +- ũ�� (���簢���̹Ƿ� -15 ~ 15)
    #endregion


    #region PLAYER SETTING
    public static int maxWeaponCount = 6; // ���� �ִ� ������ 6
    #endregion


    #region ANIMATOR PARAMETER
    
    #endregion


    #region LAYERMASK SETTING
    public static LayerMask monsterLayer = LayerMask.GetMask("Monster"); // ���� ���̾�
    #endregion
}
