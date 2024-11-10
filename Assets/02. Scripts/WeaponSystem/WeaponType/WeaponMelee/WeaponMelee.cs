using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WM_", menuName = "Scriptable Objects/Weapon/Melee")]
public class WeaponMelee : WeaponTypeDetailsSO
{
    [SerializeField] private List<BonusEffectSO> bonusEffects;
    [SerializeField] private int meleeRange = 3;


    public override void Attack(Weapon weapon)
    {
        if (weapon.DetectMonster(out Vector2 direction, out GameObject monster) == false)
            return; // 사거리 내의 가까운 적 찾기

        var colliders = Physics2D.OverlapCircleAll(monster.transform.position, meleeRange, Settings.monsterLayer);

        foreach (var collider in colliders)
        {
            var target = collider.GetComponent<Monster>();
            target.TakeDamage(weapon);
            target.Rigid.AddForce(direction * weapon.WeaponKnockback);

            foreach (var effect in bonusEffects)
            {
                effect.Apply(target);
            }
        }

        float angle = UtilitieHelper.GetAngleFromVector(direction);
        weapon.Player.WeaponTransform.RotateWeapon(weapon, angle);
        weapon.Player.WeaponTransform.MoveWeapon(weapon, direction);
    }
}
