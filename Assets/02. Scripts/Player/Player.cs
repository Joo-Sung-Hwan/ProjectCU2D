using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerDetailsSO playerDetails; // 캐릭터의 종류가 나뉘는지에 따라 필요여부 달라짐
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private PlayerStat stat; // 캐릭터 스탯
    private PlayerCtrl ctrl; // 캐릭터 컨트롤러

    #region PLAYER EVENT
    public WeaponAttackEvent WeaponAttackEvent { get; private set; }
    #endregion

    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public PlayerStat Stat => stat;
    public CircleCollider2D CircleRange { get; private set; } // 자석범위
    public HealthBarUI HealthBar {  get; private set; }
    public List<Weapon> WeaponList { get; private set; } // 무기 리스트
    public WeaponTransform WeaponTransform {  get; private set; } // 무기 장착 트랜스폼
    public CancellationTokenSource DisableCancellation { get; private set; }


    #region TEST
    public WeaponDetailsSO weapon2;
    #endregion



    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        CircleRange = GetComponentInChildren<CircleCollider2D>();
        ctrl = GetComponent<PlayerCtrl>();
        WeaponTransform = GetComponentInChildren<WeaponTransform>();
        HealthBar = GetComponent<HealthBarUI>();
        stat = new PlayerStat();

        WeaponAttackEvent = GetComponent<WeaponAttackEvent>();
    }

    private void OnEnable()
    {
        // 활성화 되면서 토큰 새롭게 초기화
        DisableCancellation = new CancellationTokenSource();
    }
    private void OnDisable()
    {
        // 비활성화 되면서 취소명령
        DisableCancellation.Cancel();
        DisableCancellation.Dispose();
    }

    private void Start()
    {
        //AddWeaponTest();
    }

    public void InitializePlayer(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;

        spriteRenderer.sprite = playerDetails.playerSprite; 
        //animator.runtimeAnimatorController = playerDetails.runtimeAnimatorController;
        WeaponList = new List<Weapon>(Settings.maxWeaponCount);
        AddWeaponToPlayer(playerDetails.playerStartingWeapon);

        stat.InitializePlayerStat(playerDetails, this);
    }

    public Weapon AddWeaponToPlayer(WeaponDetailsSO weaponDetails)
    {
        // 추가할 무기 초기화
        Weapon playerWeapon = gameObject.AddComponent<Weapon>();
        playerWeapon.InitializeWeapon(weaponDetails);
        playerWeapon.Player = this;

        // 무기 추가되면서 캐릭터 스탯 반영
        //playerWeapon.ChangeWeaponStat(PlayerStatType.BaseDamage, stat.baseDamage, false);
        //playerWeapon.ChangeWeaponStat(PlayerStatType.CriticChance, stat.criticChance, false);
        //playerWeapon.ChangeWeaponStat(PlayerStatType.CriticDamage, stat.criticDamage, false);

        WeaponList.Add(playerWeapon); // 무기 리스트에 추가
        WeaponTransform.Add(playerWeapon);

        return playerWeapon;
    }

    public void TakeDamage(float dmg)
    {
        // 스탯에서 체력 깎이는 함수 구현 (방어,회피 계산)
        stat.TakeDamage(dmg);

        if (stat.Hp <= 0f)
        {
            // 사망이벤트 처리
            
            return;
        }
    }


    #region TEST FUNCTION
    public void AddWeaponTest()
        => AddWeaponToPlayer(weapon2);
    #endregion
}
