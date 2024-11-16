using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Projectile_", menuName = "Scriptable Objects/Weapon/Projectile/Projectile")]
public class ProjectileDetailsSO : ScriptableObject
{
    // 투사체 이미지
    public Sprite sprite;

    // 투사체 적중시 효과 (폭발, 관통 등)
    public ProjectileEffectSO projectileEffect;
    // 투사체에 붙을 추가효과 (디버프, 도트데미지 등)
    public List<BonusEffectSO> bonusEffects;
    // 투사체 통과 여부
    public bool isPiercing;
    // 투사체 통과 카운트
    public int piercingCount;
    // 투사체 속도
    public int projectileSpeed = 10;
}
