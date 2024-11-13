using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private MonsterDetailsSO enemyDetails;
    private SpriteRenderer sprite;
    private MonsterMovementSO movement;
    private MonsterAttackSO monsterAttack;
    private MonsterStat stat;
    private Rigidbody2D rigid;
    private PolygonCollider2D hitbox;
    private Vector2 moveVec;

    #region MONSTER EVENT
    private MonsterDestroyedEvent monsterDestroyedEvent;
    #endregion

    public Transform Player { get; private set; }
    public ItemDetailsSO DropItem { get; private set; }
    public MonsterDetailsSO EnemyDetails => enemyDetails;
    public MonsterStat Stat => stat;
    public SpriteRenderer Sprite => sprite;
    public Rigidbody2D Rigid => rigid;

    // ���Ͱ� ��Ȱ��ȭ�Ǹ鼭 �̵�,���� �����½�ũ ����ؾ���
    public CancellationTokenSource DisableCancellation { get; private set; }



    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        hitbox = GetComponent<PolygonCollider2D>();
        monsterDestroyedEvent = GetComponent<MonsterDestroyedEvent>();
        stat = new MonsterStat();
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        // Ȱ��ȭ �Ǹ鼭 ��ū ���Ӱ� �ʱ�ȭ
        DisableCancellation?.Dispose();
        DisableCancellation = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        // ��Ȱ��ȭ �Ǹ鼭 ��Ҹ��
        DisableCancellation.Cancel();

        // Ǯ�� ���ư� �� Clone�� SO �ν��Ͻ� ����       
        Destroy(movement); // Clone�� SO �ı�
        Destroy(monsterAttack); // Clone�� SO �ı�
        movement = null;
        monsterAttack = null;
    }

    private void FixedUpdate()
    {
        movement.Move();
    }

    public void InitializeEnemy(MonsterDetailsSO enemyDetails, int waveCount)
    {
        this.enemyDetails = enemyDetails;

        stat.InitializeMonsterStat(enemyDetails, waveCount);

        Player = GameManager.Instance.Player.transform;

        sprite.sprite = enemyDetails.sprite; 
        movement = enemyDetails.movementType.Clone() as MonsterMovementSO;
        movement.InitializeMonsterMovement(this);

        monsterAttack = enemyDetails.attackType?.Clone() as MonsterAttackSO; // ����Ÿ���� �������� ����
        monsterAttack?.InitializeMonsterAttack(this);
        monsterAttack?.Attack();

        DropItem = enemyDetails.itemDetails;

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
            sprite.material = enemyDetails.enemyHitMaterial;

            await UniTask.Delay(100);

            sprite.material = enemyDetails.enemyStandardMaterial;
        }
        catch (Exception ex)
        {
            Debug.Log($"TakeDamageEffect - {ex.Message}");
        }
    }
}
