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

    // 몬스터가 비활성화되면서 이동,공격 유니태스크 취소해야함
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
        // 활성화 되면서 토큰 새롭게 초기화
        DisableCancellation?.Dispose();
        DisableCancellation = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        // 비활성화 되면서 취소명령
        DisableCancellation.Cancel();

        // 풀로 돌아갈 때 Clone된 SO 인스턴스 정리       
        Destroy(movement); // Clone된 SO 파괴
        Destroy(monsterAttack); // Clone된 SO 파괴
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

        monsterAttack = enemyDetails.attackType?.Clone() as MonsterAttackSO; // 공격타입은 없을수도 있음
        monsterAttack?.InitializeMonsterAttack(this);
        monsterAttack?.Attack();

        DropItem = enemyDetails.itemDetails;

        List<Vector2> spritePhysicsShapePointsList = new List<Vector2>();
        sprite.sprite.GetPhysicsShape(0, spritePhysicsShapePointsList); // 스프라이트 테두리 따오기
        hitbox.points = spritePhysicsShapePointsList.ToArray(); // 피격판정 충돌체 그리기
    }

    public void TakeDamage(Weapon weapon)
    {
        stat.Hp -= weapon.WeaponDamage;

        if (stat.Hp <= 0f)
        {
            // 사망이벤트 처리
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
