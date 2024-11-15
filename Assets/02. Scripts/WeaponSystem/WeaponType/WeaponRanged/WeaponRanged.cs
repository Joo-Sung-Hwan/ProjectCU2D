using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WR_", menuName = "Scriptable Objects/Weapon/Ranged")]
public class WeaponRanged : WeaponTypeDetailsSO
{
    [SerializeField] private ProjectilePatternSO pattern;
    [SerializeField] private ProjectileDetailsSO projectileDetails;


    public override void Attack(Weapon weapon)
    {
        if (weapon.DetectorType.DetectMonster(weapon, out Vector2 direction, out GameObject monster) == false)
            return; // 사거리 내의 적 찾기 (탐지 기준은 무기마다 설정가능)

        pattern.ProjectileLaunch(projectileDetails, direction, weapon);

        float angle = UtilitieHelper.GetAngleFromVector(direction);
        weapon.Player.WeaponTransform.RotateWeapon(weapon, angle);
    }
}