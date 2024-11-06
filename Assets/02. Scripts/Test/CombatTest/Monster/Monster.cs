using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.InputManagerEntry;

public class Monster : MonoBehaviour
{
    private MonsterDetailsSO enemyDetails;
    private MonsterStat stat;
    private Transform player;
    private Rigidbody2D rigid;
    private Vector2 moveVec;

    public MonsterDetailsSO EnemyDetails => enemyDetails;
    public MonsterStat Stat => stat;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameManager.Instance.Player.transform;
    }

    private void FixedUpdate()
    {
        moveVec = (player.position - transform.position).normalized;

        //rigid.velocity = moveVec * stat.Speed * Time.fixedDeltaTime;
        rigid.velocity = moveVec * 3.0f;
    }

    public void InitEnemy(MonsterDetailsSO enemyDetails)
    {
        stat = new MonsterStat(enemyDetails);

        player = GameManager.Instance.Player.transform;
    }

    public void TakeDamage(Weapon weapon)
    {
        Debug.Log($"Monster TakeDamage!!! - {weapon.WeaponDamage}");

    }
}
