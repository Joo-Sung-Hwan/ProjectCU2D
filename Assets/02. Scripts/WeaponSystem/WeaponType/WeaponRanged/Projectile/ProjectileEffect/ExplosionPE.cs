using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Explosion_", menuName = "Scriptable Objects/Weapon/Projectile/PE/Explosion")]
public class ExplosionPE : ProjectileEffectSO
{
    [SerializeField] private int explosionRange;
    private HitEffect hitEffect;

    public override void Apply(Monster monster, Weapon weapon, Vector2 direction)
    {
        var colliders = Physics2D.OverlapCircleAll(monster.transform.position, explosionRange, Settings.monsterLayer);

        foreach (var collider in colliders)
        {
            var target = collider.GetComponent<Monster>();
            target.TakeDamage(weapon, weapon.Player.Stat.RangeDamage);
            target.Rigid.AddForce(direction * weapon.WeaponKnockback);

            hitEffect = ObjectPoolManager.Instance.Get(weapon.WeaponParticle, target.transform.position, Quaternion.identity).GetComponent<HitEffect>();
            hitEffect.InitializeHitEffect(weapon.WeaponParticle);

            foreach (var effect in bonusEffects)
            {
                effect.Apply(target);
            }
        }
    }
}
