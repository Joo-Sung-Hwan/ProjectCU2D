using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        if (weapon.DetectMonster(out Vector2 direction) == false) return; // ��Ÿ� ���� ����� �� ã��

        var projectile = ObjectPoolManager.Instance.Get("bullet", weapon.transform); // 
        projectile.GetComponent<Projectile>().SetUp(weapon.WeaponAmmoSpeed, isPiercing, direction, weapon);
        
        //var socket = weapon.Player.GetTransformSocket(Settings.shootPoint);
        //projectile.transform.position = socket.position;
    }

}

