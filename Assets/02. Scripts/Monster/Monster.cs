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
    private MonsterMovementSO movement;
    private MonsterStat stat;
    private Rigidbody2D rigid;
    private PolygonCollider2D hitbox;
    private Vector2 moveVec;

    #region MONSTER EVENT
    private MonsterDestroyedEvent monsterDestroyedEvent;
    #endregion

    public Transform Player { get; private set; }
    public MonsterDetailsSO EnemyDetails => enemyDetails;
    public MonsterStat Stat => stat;
    public SpriteRenderer Sprite => sprite;
    public Rigidbody2D Rigid => rigid;



    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        hitbox = GetComponent<PolygonCollider2D>();
        monsterDestroyedEvent = GetComponent<MonsterDestroyedEvent>();
        Player = GameManager.Instance.Player.transform;
        stat = new MonsterStat();
    }

    private void Start()
    {
    }

    private void OnDisable()
    {
        // Ǯ�� ���ư� �� Clone�� SO �ν��Ͻ� ����
        if (movement != null)
        {
            Destroy(movement); // Clone�� SO �ı�
            movement = null;
        }
    }

    private void FixedUpdate()
    {
        movement.Move();
    }

    public void InitializeEnemy(MonsterDetailsSO enemyDetails)
    {
        stat.InitializeMonsterStat(enemyDetails);

        sprite.sprite = enemyDetails.sprite;
        movement = enemyDetails.movementType.Clone() as MonsterMovementSO;
        movement.InitializeMonsterMovement(this);

        List<Vector2> spritePhysicsShapePointsList = new List<Vector2>();
        sprite.sprite.GetPhysicsShape(0, spritePhysicsShapePointsList); // ��������Ʈ �׵θ� ������
        hitbox.points = spritePhysicsShapePointsList.ToArray(); // �ǰ����� �浹ü �׸���
    }

    public void TakeDamage(Weapon weapon)
    {
        stat.Hp -= weapon.WeaponDamage;

        if (stat.Hp <= 0f)
        {
            // ����̺�Ʈ ó��
            monsterDestroyedEvent.CallMonsterDestroyedEvent(this.transform.position);
            return;
        }

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
