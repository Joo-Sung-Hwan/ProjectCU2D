using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class WeaponTypeGun : WeaponType
{
    // ����ü ������
    [SerializeField] private GameObject projectilePrefab;
    // ����ü ��� ����
    [SerializeField] private bool isPiercing;

    private Vector2 direction;


    public override void Attack(Weapon weapon)
    {
        if (weapon.DetectMonster(out Vector2 direction, out GameObject monster) == false)
            return; // ��Ÿ� ���� ����� �� ã��

        var projectile = ObjectPoolManager.Instance.Get("bullet", weapon.transform); // 
        projectile.GetComponent<Projectile>().SetUp(weapon.WeaponAmmoSpeed, isPiercing, direction, weapon);

        float angle = UtilitieHelper.GetAngleFromVector(direction);
        weapon.Player.WeaponTransform.RotateWeapon(weapon, angle);
    }
}

