using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WR_", menuName = "Scriptable Objects/Weapon/Ranged")]
public class WeaponRanged : WeaponTypeDetailsSO
{
    [SerializeField] private ProjectileDetailsSO projectileDetails;

    //[SerializeReference, SubclassSelector] 
    public ProjectilePatternSO pattern;


    public override void Attack(Weapon weapon)
    {
        if (weapon.DetectMonster(out Vector2 direction, out GameObject monster) == false)
            return; // 사거리 내의 가까운 적 찾기

        pattern.ProjectileLaunch(projectileDetails, direction, weapon);

        float angle = UtilitieHelper.GetAngleFromVector(direction);
        weapon.Player.WeaponTransform.RotateWeapon(weapon, angle);
    }
}