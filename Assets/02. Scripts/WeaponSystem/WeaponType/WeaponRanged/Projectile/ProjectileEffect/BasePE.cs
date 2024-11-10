using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Base_", menuName = "Scriptable Objects/Weapon/Projectile/PE/Base")]
public class BasePE : ProjectileEffectSO
{
    public override void Apply(Monster monster, Weapon weapon, Vector2 direction)
    {
        monster.TakeDamage(weapon);
        monster.Rigid.AddForce(direction * weapon.WeaponKnockback);
     
        foreach (var effect in bonusEffects)
        {
            effect.Apply(monster);
        }
    }
}
