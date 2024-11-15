using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Base_", menuName = "Scriptable Objects/Weapon/Projectile/PE/Base")]
public class BasePE : ProjectileEffectSO
{
    private HitEffect hitEffect;


    public override void Apply(Monster monster, Weapon weapon, Vector2 direction)
    {
        monster.TakeDamage(weapon);
        monster.Rigid.AddForce(direction * weapon.WeaponKnockback);

        hitEffect = ObjectPoolManager.Instance.Get(weapon.WeaponParticle, monster.transform.position, Quaternion.identity).GetComponent<HitEffect>();
        hitEffect.InitializeHitEffect(weapon.WeaponParticle);

        foreach (var effect in bonusEffects)
        {
            effect.Apply(monster);
        }
    }
}
