using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private MonsterDetailsSO enemyDetails;
    private SpriteRenderer sprite;
    private MonsterStat stat;
    private Rigidbody2D rigid;
    private Vector2 moveVec;
    private Transform player;

    #region MONSTER EVENT
    private MonsterDestroyedEvent monsterDestroyedEvent;
    #endregion

    public MonsterDetailsSO EnemyDetails => enemyDetails;
    public MonsterStat Stat => stat;
    public Rigidbody2D Rigid => rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        monsterDestroyedEvent = GetComponent<MonsterDestroyedEvent>();
        stat = new MonsterStat();
    }

    private void Start()
    {
        player = GameManager.Instance.Player.transform;
    }

    private void FixedUpdate()
    {
        moveVec = (player.position - transform.position).normalized;

        rigid.velocity = moveVec * stat.Speed;
    }

    public void InitializeEnemy(MonsterDetailsSO enemyDetails)
    {
        stat.InitializeMonsterStat(enemyDetails);

        player = GameManager.Instance.Player.transform;
        sprite.sprite = enemyDetails.sprite;
    }

    public void TakeDamage(Weapon weapon)
    {
        if (stat.Hp <= 0f)
        {
            // 사망이벤트 처리
            monsterDestroyedEvent.CallMonsterDestroyedEvent(this.transform.position);
            return;
        }

        Debug.Log($"TakeDamage!!! - {weapon.WeaponName} , {weapon.WeaponDamage}");

        stat.Hp -= weapon.WeaponDamage;
        TakeDamageEffect().Forget();
    }

    private async UniTaskVoid TakeDamageEffect()
    {
        try
        {
            sprite.color = Color.red;

            await UniTask.Delay(100);

            sprite.color = Color.white;
        }
        catch (Exception ex)
        {
            Debug.Log($"TakeDamageEffect - {ex.Message}");
        }
    }
}
