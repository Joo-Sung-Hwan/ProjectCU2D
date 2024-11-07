using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float speed;
    private Weapon weapon;
    private bool isPiercing;
    //private SoundEffectSO soundEffect;

    private float distance;
    private float currentDistance;
    private Vector2 direction;
    private Vector2 distanceVector;


    public void SetUp(float speed, bool isPiercing, Vector2 direction, Weapon weapon)
    {
        //soundEffect = weapon.weaponFiringSoundEffect;
        this.speed = speed;
        this.isPiercing = isPiercing;
        this.direction = direction; // 투사체의 전방 설정
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
            monster.TakeDamage(weapon);
            monster.Rigid.AddForce(direction * weapon.WeaponKnockback);
        }

        // 관통되는 수를 정하고 싶으면 관통되는 적의 수를 count로 세면 됨
        // 지금은 관통되는지 여부에 따라 모두 관통이거나 안되거나만 설정
        if (!isPiercing)
            ObjectPoolManager.Instance.Release(this.gameObject, "bullet");
    }
}
