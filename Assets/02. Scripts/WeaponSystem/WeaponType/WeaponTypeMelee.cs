using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponTypeMelee : WeaponType
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

        var target = monster.GetComponent<Monster>();
        target.TakeDamage(weapon);
        target.Rigid.AddForce(direction * weapon.WeaponKnockback);

        float angle = UtilitieHelper.GetAngleFromVector(direction);
        weapon.Player.WeaponTransform.RotateWeapon(weapon, angle);
    }
}
