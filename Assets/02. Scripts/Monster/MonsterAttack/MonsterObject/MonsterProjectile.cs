using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float speed;
    private float dmg;

    private float currentDistance;
    private Vector2 direction;
    private Vector2 distanceVector;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void InitializeMonsterProjectile(float speed, Vector2 moveVec, float dmg)
    {
        this.speed = speed;
        this.direction = moveVec; // 투사체 방향벡터
        this.dmg = dmg;
        currentDistance = 0f;
    }


    private void FixedUpdate()
    {
        // FixedUpdate에서 물리적 연산으로 전방을 향해 speed만큼 이동
        distanceVector = direction * speed;
        currentDistance += distanceVector.magnitude * Time.fixedDeltaTime; // 날아간 거리계산을 위한 magnitude
        rigidBody.velocity = distanceVector;

        if (currentDistance > Settings.monsterProjectileDist)
            ObjectPoolManager.Instance.Release(this.gameObject, "MonsterBullet");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(dmg);
        }

        ObjectPoolManager.Instance.Release(this.gameObject, "MonsterBullet");
    }
}
