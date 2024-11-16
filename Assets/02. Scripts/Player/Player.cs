using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerDetailsSO playerDetails; // ĳ������ ������ ���������� ���� �ʿ俩�� �޶���
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private CircleCollider2D circleRange; // �ڼ�����
    private PlayerStat stat; // ĳ���� ����
    private PlayerCtrl ctrl; // ĳ���� ��Ʈ�ѷ�

    #region PLAYER EVENT
    public WeaponAttackEvent WeaponAttackEvent { get; private set; }
    private WeaponAttack weaponAttack;
    #endregion

    public PlayerStat Stat => stat;
    public HealthBarUI HealthBar {  get; private set; }
    public List<Weapon> WeaponList { get; private set; } // ���� ����Ʈ
    public WeaponTransform WeaponTransform {  get; private set; } // ���� ���� Ʈ������
    public CancellationTokenSource DisableCancellation { get; private set; }


    #region TEST
    public WeaponDetailsSO weapon2;
    #endregion



    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleRange = GetComponentInChildren<CircleCollider2D>();
        ctrl = GetComponent<PlayerCtrl>();
        WeaponTransform = GetComponentInChildren<WeaponTransform>();
        HealthBar = GetComponent<HealthBarUI>();
        stat = new PlayerStat();

        WeaponAttackEvent = GetComponent<WeaponAttackEvent>();
        weaponAttack = GetComponent<WeaponAttack>();       
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
        DisableCancellation.Dispose();
    }

    private void Start()
    {
        //AddWeaponTest();
    }

    public void InitializePlayer(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;

        // spriteRenderer.sprite = playerDetails.playerSprite; // ���� ĳ���� ��������Ʈ�� �غ�ȵ�
        //animator.runtimeAnimatorController = playerDetails.runtimeAnimatorController;
        WeaponList = new List<Weapon>(Settings.maxWeaponCount);
        AddWeaponToPlayer(playerDetails.playerStartingWeapon);

        stat.InitializePlayerStat(playerDetails, this);
    }

    public Weapon AddWeaponToPlayer(WeaponDetailsSO weaponDetails)
    {
        // �߰��� ���� �ʱ�ȭ
        Weapon playerWeapon = gameObject.AddComponent<Weapon>();
        playerWeapon.InitializeWeapon(weaponDetails);
        playerWeapon.Player = this;

        // ���� �߰��Ǹ鼭 ĳ���� ���� �ݿ�
        //playerWeapon.ChangeWeaponStat(PlayerStatType.BaseDamage, stat.baseDamage, false);
        //playerWeapon.ChangeWeaponStat(PlayerStatType.CriticChance, stat.criticChance, false);
        //playerWeapon.ChangeWeaponStat(PlayerStatType.CriticDamage, stat.criticDamage, false);

        WeaponList.Add(playerWeapon); // ���� ����Ʈ�� �߰�
        WeaponTransform.Add(playerWeapon);

        return playerWeapon;
    }

    public void TakeDamage(float dmg)
    {
        // ���ȿ��� ü�� ���̴� �Լ� ���� (���,ȸ�� ���)
        stat.TakeDamage(dmg);

        if (stat.Hp <= 0f)
        {
            // ����̺�Ʈ ó��
            
            return;
        }
    }


    #region TEST FUNCTION
    public void AddWeaponTest()
        => AddWeaponToPlayer(weapon2);
    #endregion
}
