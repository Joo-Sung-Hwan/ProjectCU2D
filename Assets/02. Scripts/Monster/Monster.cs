using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviourPun
{
    [SerializeField] private Database monsterDB;

    private MonsterDetailsSO enemyDetails;
    private SpriteRenderer sprite;
    private MonsterMovementSO movement;
    private MonsterAttackSO monsterAttack;
    private MonsterStat stat;
    private Rigidbody2D rigid;
    private PolygonCollider2D hitbox;

    #region MONSTER EVENT
    private MonsterDestroyedEvent monsterDestroyedEvent;
    #endregion

    public Transform Player { get; private set; }
    public int DropItem { get; private set; }
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
        DisableCancellation = new CancellationTokenSource();
    }

    private void OnDisable()
    {
    }

    private void FixedUpdate()
    {
        movement?.Move();
    }

    [PunRPC]
    public void InitializeEnemy(int id, int waveCount)
    {
        MonsterDetailsSO enemyDetails = monsterDB.GetDataByID<MonsterDetailsSO>(id);

        Player = GameManager.Instance.Player.transform;
        this.enemyDetails = enemyDetails;
        stat.InitializeMonsterStat(enemyDetails, waveCount); // 현재 웨이브에 맞추어 스탯초기화

        sprite.sprite = enemyDetails.sprite;
        rigid.freezeRotation = true;
        transform.localScale = Vector3.one;
        hitbox.enabled = true;

        movement = enemyDetails.movementType.Clone() as MonsterMovementSO;
        movement.InitializeMonsterMovement(this);

        monsterAttack = enemyDetails.attackType?.Clone() as MonsterAttackSO; // 공격타입은 없을수도 있음
        monsterAttack?.InitializeMonsterAttack(this);
        monsterAttack?.Attack(); // 몬스터 활성화되면서 공격시작

        DropItem = enemyDetails.itemDetails.ID;

        List<Vector2> spritePhysicsShapePointsList = new List<Vector2>();
        sprite.sprite.GetPhysicsShape(0, spritePhysicsShapePointsList); // 스프라이트 테두리 따오기
        hitbox.points = spritePhysicsShapePointsList.ToArray(); // 피격판정 충돌체 그리기
    }

    public void TakeDamage(Weapon weapon, int bonusDamage = 0)
    {
        stat.Hp -= GetDamage(weapon, bonusDamage);

        if (stat.Hp <= 0f)
        {
            // 비활성화 되면서 취소명령
            DisableCancellation.Cancel();
            DisableCancellation.Dispose();

            // 풀로 돌아갈 때 Clone된 SO 인스턴스 정리       
            Destroy(movement); // Clone된 SO 파괴
            Destroy(monsterAttack); // Clone된 SO 파괴
            movement = null;
            monsterAttack = null;

            hitbox.enabled = false;

            // 사망이벤트 처리
            monsterDestroyedEvent.CallMonsterDestroyedEvent(this.transform.position);
            return;
        }

        TakeDamageEffect().Forget();
    }

    private int GetDamage(Weapon weapon, int bonusDamage)
    {
        // 플레이어의 근/원거리 데미지와 추가데미지% 계산
        int damage = UtilitieHelper.IncreaseByPercent(weapon.WeaponDamage + bonusDamage, weapon.Player.Stat.BonusDamage);

        // 치명타 성공
        if (UtilitieHelper.isSuccess(weapon.WeaponCriticChance))
        {
            damage = UtilitieHelper.IncreaseByPercent(damage, weapon.WeaponCriticDamage);

            HitTextUI hitText = ObjectPoolManager.Instance.Get("HitText", new Vector2(transform.position.x, transform.position.y + 0.75f), Quaternion.identity).GetComponent<HitTextUI>();
            hitText.InitializeHitText(damage, true);
        }
        else
        {
            HitTextUI hitText = ObjectPoolManager.Instance.Get("HitText", new Vector2(transform.position.x, transform.position.y + 0.75f), Quaternion.identity).GetComponent<HitTextUI>();
            hitText.InitializeHitText(damage);
        }

        return damage;
    }

    private async UniTaskVoid TakeDamageEffect()
    {
        try
        {
            // 피격시 0.1초 동안 흰색 머테리얼로 변경
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
