using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class WeaponTypeGun : WeaponType
{
    // 투사체 프리팹
    [SerializeField] private GameObject projectilePrefab;
    // 투사체 통과 여부
    [SerializeField] private bool isPiercing;

    private Vector2 direction;


    public override void Attack(Weapon weapon)
    {
        if (weapon.DetectMonster(out Vector2 direction, out GameObject monster) == false)
            return; // 사거리 내의 가까운 적 찾기

        var projectile = ObjectPoolManager.Instance.Get("bullet", weapon.transform); // 
        projectile.GetComponent<Projectile>().SetUp(weapon.WeaponAmmoSpeed, isPiercing, direction, weapon);

        float angle = UtilitieHelper.GetAngleFromVector(direction);
        weapon.Player.WeaponTransform.RotateWeapon(weapon, angle);
    }
}

