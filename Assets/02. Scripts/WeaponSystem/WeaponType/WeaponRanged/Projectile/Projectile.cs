using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private ProjectileEffectSO projectileEffect;
    //private List<BonusEffectSO> bonusEffects = new List<BonusEffectSO>();

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Weapon weapon;
    private float speed;
    private bool isPiercing;
    // private int piercingCount; // 관통 카운트
    //private SoundEffectSO soundEffect;

    private float distance;
    private float currentDistance;
    private Vector2 direction;
    private Vector2 distanceVector;


    public void InitializeProjectile(ProjectileDetailsSO projectileDetails, Vector2 direction, Weapon weapon)
    {
        //soundEffect = weapon.weaponFiringSoundEffect;
        this.projectileEffect = projectileDetails.projectileEffect;
        projectileEffect.InitializePE(projectileDetails.bonusEffects);
        this.speed = projectileDetails.projectileSpeed;
        this.isPiercing = projectileDetails.isPiercing;
        this.direction = direction; // 투사체 방향벡터
        this.weapon = weapon;
        distance = weapon.WeaponRange;
        currentDistance = 0f;
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // FixedUpdate에서 물리적 연산으로 전방을 향해 speed만큼 이동
        distanceVector = direction * speed;
        currentDistance += distanceVector.magnitude * Time.fixedDeltaTime; // 날아간 거리계산을 위한 magnitude
        rigidBody.velocity = distanceVector;

        if (currentDistance > distance)
            ObjectPoolManager.Instance.Release(this.gameObject, "bullet");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Monster monster))
        {
            //SoundEffectManager.Instance.PlaySoundEffect(soundEffect);
            projectileEffect.Apply(monster, weapon, direction);

            // TEST
            HitEffect hitEffect = ObjectPoolManager.Instance.Get(weapon.WeaponDetails.weaponParticle.name, monster.transform.position, Quaternion.identity).GetComponent<HitEffect>();
            hitEffect.InitializeHitEffect(weapon.WeaponDetails.weaponParticle.name);
        }

        // 관통되는 수를 정하고 싶으면 관통되는 적의 수를 count로 세면 됨
        // 지금은 관통되는지 여부에 따라 모두 관통이거나 안되거나만 설정
        if (!isPiercing)
            ObjectPoolManager.Instance.Release(this.gameObject, "bullet");
    }
}
